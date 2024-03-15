using Carter;
using OrderFlow.Application;
using OrderFlow.Infrastructure;
using OverFlow.Presentation;
using OverFlow.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation(builder.Configuration);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.UseAuthentication();

// Mapper for minimal apis
app.MapCarter();

app.Run();