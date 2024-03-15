namespace OrderFlow.Domain.Dtos;

public record EmailMessage
(
    string? UserEmail,
    string? UserName,
    string Content,
    string Subject
);