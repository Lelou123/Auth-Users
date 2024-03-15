﻿using MediatR;
using OrderFlow.Domain.Dtos;

namespace OrderFlow.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Result>
    where TQuery : IQuery;


public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<Result<TResponse>>;
    