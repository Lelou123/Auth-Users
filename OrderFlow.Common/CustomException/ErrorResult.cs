namespace OrderFlow.Common.CustomException;

public class ErrorResult(
    string message,
    bool success
)
{
    public string Message { get; set; } = message;
    public bool Success { get; set; } = success;
}