using MediatR;
using OrderFlow.Domain.Dtos;

namespace OrderFlow.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<out TResponse> : IRequest<TResponse>;