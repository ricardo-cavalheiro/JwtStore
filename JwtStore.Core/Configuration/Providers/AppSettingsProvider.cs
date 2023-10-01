namespace JwtStore.Core.Configuration.Providers;

public class DatabaseConfiguration
{
  public string Key { get; private set; } = "DefaultConnection";

  public string ConnectionString { get; set; } = string.Empty;
}
