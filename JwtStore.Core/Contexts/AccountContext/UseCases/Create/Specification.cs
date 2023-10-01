using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
  public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
        .Requires()
        .IsLowerThan(request.Name.Length, 160, "Name", "O Nome deve ser menos que 160 caracteres.")
        .IsGreaterThan(request.Name.Length, 3, "Name", "O Nome deve ser maior que 3 caracteres.")
        .IsLowerThan(request.Password.Length, 40, "Password", "A Senha deve ser menor que 60 caracteres.")
        .IsGreaterThan(request.Password.Length, 8, "Password", "A Senha deve ser maior que 8 caracteres.")
        .IsEmail(request.Email, "Email", "E-mail inválido");
}