namespace JwtStore.Core.Configuration.Providers;

public class DatabaseParamsConfiguration
{
  public string Key { get; private set; } = "DatabaseParams";
  public string Password { get; set; } = string.Empty;
}

public class SecretsConfiguration
{
  public string Key { get; private set; } = "Secrets";

  public string ApiKey { get; set; } = string.Empty;

  public string JwtPrivateKey { get; set; } = string.Empty;

  public string PasswordSaltKey { get; set; } = string.Empty;
}

public class EmailConfiguration
{
  public string Key { get; private set; } = "Email";

  public string DefaultFromEmail { get; set; } = string.Empty;

  public string DefaultFromName { get; set; } = string.Empty;
}

public class SendGridConfiguration
{
  public string Key { get; private set; } = "SendGrid";

  public string ApiKey { get; set; } = string.Empty;
}