using Lipip.Atomic.EntityFramework.Common.Audit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lipip.Atomic.EntityFramework.Behaviors.Atomics
{
    public class AtomicTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IAuditService _auditService;
        private readonly DbContext _context;
        private readonly ILogger<AtomicTransactionBehavior<TRequest, TResponse>> _logger;

        public AtomicTransactionBehavior(
            DbContext context,
            ILogger<AtomicTransactionBehavior<TRequest, TResponse>> logger,
            IAuditService auditService)
        {
            _context = context;
            _logger = logger;
            _auditService = auditService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
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

                if (_context.ChangeTracker.HasChanges())
                {
                    _auditService.ApplyAudit(_context);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch 
            {
                await transaction.RollbackAsync(cancellationToken);
                //_logger.LogError(ex, "Transaction failed for request {RequestName}", typeof(TRequest).Name);
                throw;
            }
        }
    }
}