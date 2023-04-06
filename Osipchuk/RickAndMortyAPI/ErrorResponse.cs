using Newtonsoft.Json;
using System.Net;

namespace RickAndMortyAPI
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ErrorResponse(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            string error = JsonConvert.SerializeObject(this);
            return error;
        }
    }
}
