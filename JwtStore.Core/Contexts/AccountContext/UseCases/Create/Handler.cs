using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
  private readonly IRepository _repository;
  private readonly IService _service;

  public Handler(IRepository repository, IService service)
  {
    _repository = repository;
    _service = service;
  }

  public async Task<Response> Handle(
    Request request,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var requestValidation = Specification.Ensure(request);

      if (!requestValidation.IsValid)
      {
        return new Response("Requisição inválida", 400, requestValidation.Notifications);
      }
    }
    catch (Exception)
    {
      return new Response("Não foi possível validar a requisição.", 500);
    }

    Email email;
    Password password;
    User user;

    try
    {
      email = new Email(request.Email);
      password = new Password(request.Password);
      user = new User(request.Name, email, password);
    }
    catch (Exception ex)
    {
      return new Response(ex.Message, 400);
    }

    try
    {
      var exists = await _repository.AnyAsync(request.Email, cancellationToken);

      if (exists)
      {
        return new Response("Este e-mail já está em uso.", 400);
      }
    }
    catch (Exception ex)
    {
      return new Response(ex.Message, 500);
    }

    try
    {
      await _repository.SaveAsync(user, cancellationToken);
    }
    catch (Exception)
    {
      return new Response("Falha ao persistir dados.", 500);
    }

    try
    {
      await _service.SendVerificationEmailAsync(user, cancellationToken);
    }
    finally { /* Do nothing */ }

    return new Response("Conta criada.", new ResponseData(user.Id, user.Name, user.Email));
  }
}
