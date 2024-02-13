using System.Net;

namespace OrderFlow.Common.CustomException;

public class CustomException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Errors { get; set; }

    public CustomException(HttpStatusCode statusCode, string? errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}