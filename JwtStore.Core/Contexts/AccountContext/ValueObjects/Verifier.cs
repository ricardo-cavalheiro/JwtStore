using JwtStore.Core.Contexts.SharedContext.ValueObjects;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public class Verifier : ValueObject
{
  public string Code { get; private set; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();

  public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);

  public DateTime? VerifiedAt { get; private set; } = null;

  public bool IsActive => VerifiedAt != null && ExpiresAt == null;

  public Verifier() { }

  public void Verify(string code)
  {
    if (IsActive)
    {
      throw new Exception("Este código já foi ativado.");
    }

    if (ExpiresAt < DateTime.UtcNow)
    {
      throw new Exception("Este item já expirou.");
    }

    if (!string.Equals(code.Trim(), Code, StringComparison.CurrentCultureIgnoreCase))
    {
      throw new Exception("Código inválido.");
    }

    ExpiresAt = null;
    VerifiedAt = DateTime.UtcNow;
  }
}
