using MediatR;
using OrderFlow.Domain.Abstractions;

namespace OrderFlow.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;