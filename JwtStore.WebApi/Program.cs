using JwtStore.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddAccountContext();
builder.AddMediator();

var app = builder.Build();

if (app.Environment.IsProduction())
{
  app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountEndpoints();

app.Run();
