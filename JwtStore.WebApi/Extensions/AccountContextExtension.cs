using MediatR;

using CoreCreate = JwtStore.Core.Contexts.AccountContext.UseCases.Create;
using InfraCreate = JwtStore.Infra.Contexts.AccountContext.UseCases.Create;
using CoreAuthenticate = JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;
using InfraAuthenticate = JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate;

namespace JwtStore.WebApi.Extensions;

public static class AccountContextExtension
{
  public static void AddAccountContext(this WebApplicationBuilder builder)
  {
    builder
      .Services
      .AddTransient<CoreCreate.Contracts.IRepository, InfraCreate.Repository>()
      .AddTransient<CoreCreate.Contracts.IService, InfraCreate.Service>();

    builder
      .Services
      .AddTransient<CoreAuthenticate.Contracts.IRepository, InfraAuthenticate.Repository>();
  }

  public static void MapAccountEndpoints(this WebApplication app)
  {
    app
      .MapPost(
        "api/v1/users",
        async (
          CoreCreate.Request request,
          IRequestHandler<CoreCreate.Request, CoreCreate.Response> handler
        ) =>
        {
          var result = await handler.Handle(request, new CancellationToken());

          return result.IsSuccess
            ? Results.Created($"api/v1/users/{result.Data?.Id}", result)
            : Results.Json(result, statusCode: result.Status);
        }
      )
      .AllowAnonymous();

    app
      .MapPost(
        "api/v1/authenticate",
        async (
          CoreAuthenticate.Request request,
          IRequestHandler<CoreAuthenticate.Request, CoreAuthenticate.Response> handler
        ) =>
        {
          var result = await handler.Handle(request, new CancellationToken());

          if (!result.IsSuccess)
          {
            return Results.Json(result, statusCode: result.Status);
          }

          if (result.Data is null)
          {
            return Results.Json(result, statusCode: 500);
          }

          result.Data.Token = JwtExtension.Generate(result.Data);

          return Results.Ok(result);
        }
      )
      .AllowAnonymous();
  }
}