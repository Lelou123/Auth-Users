using MediatR;
using OrderFlow.Domain.Dtos;

namespace OrderFlow.Application.Abstractions.Messaging;

public interface IQuery : IRequest<Result>;

public interface IQuery<out TResponse> : IRequest<TResponse>;