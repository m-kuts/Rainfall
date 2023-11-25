using System.Net;

namespace RainfallApi.Exceptions;

public class StatusCodeException : Exception
{
    public StatusCodeException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}
