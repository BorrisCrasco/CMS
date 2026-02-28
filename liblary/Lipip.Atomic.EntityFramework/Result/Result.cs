using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Result
{
    public class Result : IResult
    {
        public bool IsSuccess { get; protected set; }
        public string? ErrorMessage { get; protected set; }
        public int? StatusCode { get; protected set; }

        public Dictionary<string, string[]>? Errors { get; protected set; }

        public static IResult Success()
            => new Result { IsSuccess = true };

        public static IResult<T> Success<T>(T value)
            => Result<T>.Success(value);

        public static IResult Failure(string error, int? statusCode = null)
            => new Result { IsSuccess = false, ErrorMessage = error, StatusCode = statusCode };

        public static IResult BadRequest(string error) => Failure(error, 400);
        public static IResult NotFound(string error) => Failure(error, 404);
        public static IResult Unauthorized(string error) => Failure(error, 401);
        public static IResult ValidationError(Dictionary<string, string[]> errors)
            => new Result
            {
                IsSuccess = false,
                StatusCode = 422,
                Errors = errors
            };
    }


    public class Result<T> : Result, IResult<T>
    {
        public T? Value { get; private set; }

        public static IResult<T> Success(T value)
            => new Result<T> { IsSuccess = true, Value = value };

        public static new IResult<T> Failure(string error, int? statusCode = null)
            => new Result<T> { IsSuccess = false, ErrorMessage = error, StatusCode = statusCode };

        public static IResult<T> BadRequest(string error) => Failure(error, 400);
        public static IResult<T> NotFound(string error) => Failure(error, 404);
        public static IResult<T> Unauthorized(string error) => Failure(error, 401);

        public static IResult<T> ValidationError(Dictionary<string, string[]> errors)
            => new Result<T>
            {
                IsSuccess = false,
                StatusCode = 422,
                Errors = errors
            };
    }

}
