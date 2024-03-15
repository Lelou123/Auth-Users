using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Dtos;

namespace OrderFlow.Domain.Interfaces.Services;

public interface IEmailService
{
    Task<Result> SendEmail(EmailMessage message);
}