namespace OrderFlow.Domain.Enums;

public enum EnOrderStatus
{
    Received = 1,
    Preparing,
    Ready,
    OnRoute,
    Delivered,
    Cancelled,
    Delayed
}