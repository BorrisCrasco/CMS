using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Result
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string? ErrorMessage { get; }
        int? StatusCode { get; }
    }

    public interface IResult<T> : IResult
    {
        T? Value { get; }

    }
}
