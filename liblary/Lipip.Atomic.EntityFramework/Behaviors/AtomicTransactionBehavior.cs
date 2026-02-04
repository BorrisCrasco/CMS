using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Behaviors
{
    public class AtomicTransactionBehavior
        <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly DbContext _context;
        private readonly ILogger<AtomicTransactionBehavior<TRequest, TResponse>> _logger;

        public AtomicTransactionBehavior(DbContext context, ILogger<AtomicTransactionBehavior<TRequest, TResponse>> logger)
        {
            _context = context;
            _logger = logger;
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
    }
}
