using Lipip.Atomic.EntityFramework.Common.Authentications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Behaviors.Atomics
{
    public class AtomicTransactionBehavior
        <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly DbContext _context;
        private readonly ILogger<AtomicTransactionBehavior<TRequest, TResponse>> _logger;

        public AtomicTransactionBehavior(DbContext context, ILogger<AtomicTransactionBehavior<TRequest, TResponse>> logger, ICurrentUser currentUser)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {


            if (_context.Database.CurrentTransaction != null)
            {
                return await next();
            }

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await next();

                if (response is Result.IResult result && !result.IsSuccess)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return response;
                }

                ApplyAutomaticAudit();

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Transaction failed for {Request}", typeof(TRequest).Name);
                throw;
            }
        }

        private void ApplyAutomaticAudit()
        {
            var entries = _context.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted);

            var now = DateTime.UtcNow;
            var user = _currentUser.Username ?? "SYSTEM";

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var type = entity.GetType();

                var createdDateProp = type.GetProperty("CreatedDate");
                var createdByProp = type.GetProperty("CreatedBy");
                var updatedDateProp = type.GetProperty("UpdatedDate");
                var updatedByProp = type.GetProperty("UpdatedBy");
                var isDeletedProp = type.GetProperty("IsDeleted");
                var deletedDateProp = type.GetProperty("DeletedDate");
                var deletedByProp = type.GetProperty("DeletedBy");

                if (entry.State == EntityState.Added)
                {
                    createdDateProp?.SetValue(entity, now);
                    createdByProp?.SetValue(entity, user);
                }

                if (entry.State == EntityState.Modified)
                {
                    updatedDateProp?.SetValue(entity, now);
                    updatedByProp?.SetValue(entity, user);
                }

                if (entry.State == EntityState.Deleted && isDeletedProp != null)
                {
                    entry.State = EntityState.Modified;

                    isDeletedProp.SetValue(entity, true);
                    deletedDateProp?.SetValue(entity, now);
                    deletedByProp?.SetValue(entity, user);
                }
            }
        }
    }
}
