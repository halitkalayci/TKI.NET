using Newtonsoft.Json;

namespace Core.Exceptions
{
    public class ErrorDetails
    {
        public object Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
