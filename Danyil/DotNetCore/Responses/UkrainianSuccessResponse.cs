using Newtonsoft.Json;
using System.Net;

namespace DotNetMentorship.TestAPI.Responses
{
    public class UkrainianSuccessResponse
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Message { get; private set; }

        public int UserId { get; private set; }

        public UkrainianSuccessResponse(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public UkrainianSuccessResponse(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            Message = message;
        }

        public UkrainianSuccessResponse(int statusCode, string statusDescription, string message, int id)
           : this(statusCode, statusDescription)
        {
            Message = message;
            UserId = id;
        }

        public class UkrainianObjectCreatedResponse : UkrainianSuccessResponse
        {
            public UkrainianObjectCreatedResponse(string message, int userId)
                : base(200, "Object Created", message, userId)
            {
            }
            public UkrainianObjectCreatedResponse(string message)
                : base(200, "Object Created", message)
            {
            }

        }
    }
}
