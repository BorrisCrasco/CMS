using FluentValidation;
using Lipip.Atomic.EntityFramework.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Behaviors.Validations
{
    public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull

    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var failures = (await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var errors = failures
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                var responseType = typeof(TResponse);
                var valueType = responseType.GetGenericArguments()[0];

                var genericResultType = typeof(Result<>).MakeGenericType(valueType);

                var method = genericResultType.GetMethod("ValidationError",
                    new[] { typeof(Dictionary<string, string[]>) });

                var failureResult = method.Invoke(null, new object[] { errors });

                return (TResponse)failureResult!;

                throw new InvalidOperationException("ValidationBehavior only supports IResult<T> responses.");
            }

            return await next();
        }

    }
}
