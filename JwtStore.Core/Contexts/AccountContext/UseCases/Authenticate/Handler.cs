using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
  private readonly IRepository _repository;

  private readonly ILogger<Handler> _logger;

  public Handler(IRepository repository, ILogger<Handler> logger)
    => (_repository, _logger) = (repository, logger);

  public async Task<Response> Handle(
    Request request,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var res = Specification.Ensure(request);

      if (!res.IsValid)
      {
        return new Response("Requisição inválida", 400, res.Notifications);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Não foi possível validar sua requisição");

      return new Response("Não foi possível validar sua requisição", 500);
    }

    User? user;

    try
    {
      user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

      if (user is null)
      {
        return new Response("Perfil não encontrado", 404);
      }
    }
    catch (Exception)
    {
      return new Response("Não foi possível recuperar o perfil", 500);
    }

    if (!user.Password.Challenge(request.Password))
    {
      return new Response("Usuário ou senha inválidos", 400);
    }

    try
    {
      if (!user.Email.Verifier.IsActive)
      {
        return new Response("Conta inativa", 400);
      }
    }
    catch (Exception)
    {
      return new Response("Não foi possível verificar seu perfil", 500);
    }



    try
    {
      var data = new ResponseData
      {
        Token = string.Empty,
        Id = user.Id.ToString(),
        Name = user.Name,
        Email = user.Email,
        Roles = user.Roles.Select(role => role.Name).ToArray()
      };

      return new Response(string.Empty, data);
    }
    catch (Exception)
    {
      return new Response("Não foi possível obter os dados do perfil", 500);
    }
  }
}