namespace OrderFlow.Domain.Dtos;

public class Result
{
    public bool IsSuccess { get; set; }
}


public class Result<TResponse>
{
    public bool IsSuccess { get; set; }

    public TResponse? Data { get; set; }
}