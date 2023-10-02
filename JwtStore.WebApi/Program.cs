using JwtStore.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddAccountContext();
builder.AddMediator();

var app = builder.Build();

app
  .UseExceptionHandler(
    exceptionHandlerApp
      => exceptionHandlerApp
          .Run(async context
              => await Results
                  .Problem()
                  .ExecuteAsync(context)
          )
  );
app.UseStaticFiles();

app.MapAccountEndpoints();

app.Run();
