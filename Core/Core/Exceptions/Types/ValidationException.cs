using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions.Types
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; set; }

        public ValidationException(List<string> errors, string message) : base(message)
        {
            Errors = errors;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
