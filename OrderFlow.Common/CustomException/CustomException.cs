using System.Net;

namespace OrderFlow.Common.CustomException;

public class CustomException(
    HttpStatusCode statusCode,
    string message
) : Exception(message: message);