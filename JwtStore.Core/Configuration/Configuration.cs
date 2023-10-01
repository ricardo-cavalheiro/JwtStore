using JwtStore.Core.Configuration.Providers;

namespace JwtStore.Core.Configuration;

public static class Configurations
{
  public static SecretsConfiguration Secrets { get; private set; } = new();
  public static DatabaseConfiguration Database { get; private set; } = new();
  public static EmailConfiguration Email { get; private set; } = new();
  public static SendGridConfiguration SendGrid { get; private set; } = new();
  public static DatabaseParamsConfiguration DatabaseParamsConfiguration { get; private set; } = new();
}
