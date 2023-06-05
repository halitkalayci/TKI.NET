using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public interface IResult
    {
        // Read-only
        bool Success { get; }
        string Message { get; }
    }
}
