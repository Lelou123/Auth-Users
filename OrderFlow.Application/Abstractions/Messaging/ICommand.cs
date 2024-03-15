using MediatR;
using OrderFlow.Domain.Abstractions;

namespace OrderFlow.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;