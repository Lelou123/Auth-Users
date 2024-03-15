using MediatR;
using OrderFlow.Domain.Abstractions;

namespace OrderFlow.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;