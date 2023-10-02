using System.Text;
using JwtStore.Core.Configuration;
using JwtStore.Core.Configuration.Providers;
using JwtStore.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JwtStore.WebApi.Extensions;

public static class BuilderExtension
{
  public static void AddConfiguration(this WebApplicationBuilder builder)
  {
    var secrets = new SecretsConfiguration();
    var email = new EmailConfiguration();
    var sendGrid = new SendGridConfiguration();
    var databaseParams = new DatabaseParamsConfiguration();

    builder.Configuration.GetSection(Configurations.Secrets.Key).Bind(secrets);
    builder.Configuration.GetSection(Configurations.Email.Key).Bind(email);
    builder.Configuration.GetSection(Configurations.SendGrid.Key).Bind(sendGrid);
    builder.Configuration.GetSection(Configurations.DatabaseParamsConfiguration.Key).Bind(databaseParams);

    var connectionStringBuilder = new SqlConnectionStringBuilder(
      builder.Configuration.GetConnectionString(Configurations.Database.Key)
    )
    {
      Password = databaseParams.Password,
      DataSource = databaseParams.DataSource
    };

    Configurations.Database.ConnectionString = connectionStringBuilder.ConnectionString;

    Configurations.Secrets.ApiKey = secrets.ApiKey;
    Configurations.Secrets.JwtPrivateKey = secrets.JwtPrivateKey;
    Configurations.Secrets.PasswordSaltKey = secrets.PasswordSaltKey;

    Configurations.Email.DefaultFromEmail = email.DefaultFromEmail;
    Configurations.Email.DefaultFromName = email.DefaultFromName;

    Configurations.SendGrid.ApiKey = sendGrid.ApiKey;
  }

  public static void AddDatabase(this WebApplicationBuilder builder)
  {
    builder
      .Services
      .AddDbContext<AppDbContext>(
        options => options
          .UseSqlServer(
            Configurations.Database.ConnectionString,
            b => b.MigrationsAssembly("JwtStore.WebApi")
          )
      );
  }

  public static void AddJwtAuthentication(this WebApplicationBuilder builder)
  {
    builder
      .Services
      .AddAuthentication(
        options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
      )
      .AddJwtBearer(
        options =>
        {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configurations.Secrets.JwtPrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
          };
        }
      );

    builder
      .Services
      .AddAuthorization();
  }

  public static void AddMediator(this WebApplicationBuilder builder)
  {
    builder
      .Services
      .AddMediatR(
        options => options.RegisterServicesFromAssembly(typeof(Configurations).Assembly)
      );
  }
}