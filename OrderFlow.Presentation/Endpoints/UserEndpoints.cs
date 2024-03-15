using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OrderFlow.Application.Handlers.Users.Commands.CreateUserCommand;
using OrderFlow.Domain.Abstractions;
using OverFlow.Presentation.Extensions;

namespace OverFlow.Presentation.Endpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users");

        group.MapPost(string.Empty, async ([FromBody]CreateUserCommand createUserCommand, ISender sender) => {
            Result<Guid> result = await sender.Send(createUserCommand);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}