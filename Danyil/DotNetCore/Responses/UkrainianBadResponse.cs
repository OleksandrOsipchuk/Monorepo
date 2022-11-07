using Newtonsoft.Json;
using System.Net;

namespace DotNetMentorship.TestAPI.Responses
{
    public class UkrainianBadResponse
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public UkrainianBadResponse()
        {
            StatusCode = 400;
            StatusDescription = null;
            Message = null;
        }

        public UkrainianBadResponse(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public UkrainianBadResponse(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            Message = message;
        }

        public class UkrainianValidationError : UkrainianBadResponse
        {
            public UkrainianValidationError()
                : base(500, HttpStatusCode.InternalServerError.ToString())
            {
            }


            public UkrainianValidationError(string message)
                : base(500, HttpStatusCode.InternalServerError.ToString(), message)
            {
            }
        }
    }
}
