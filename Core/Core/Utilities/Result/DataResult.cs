using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message) : base(success,message)
        {
            Data = data;
        }
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
// DataResult<List<Car>> => Data=[], Message=Arabalar listelendi, Success=true
// Result => Message=Araba silindi, Success=true
// Result => Message=Araba silinemedi bu id ile bir araba yok, Success=false
// Response.Message, Response.Success, Response.Data