## JwtStore.WebApi

1 - Environment Variables

This project uses `dotnet user-secrets` to store some env variables. The following is the list of env varibles you should set using `dotnet user-secrets`:

```
SendGrid:ApiKey *
Secrets:PasswordSaltKey
Secrets:JwtPrivateKey
Secrets:ApiKey
Email:DefaultFromName
Email:DefaultFromEmail
DatabaseParams:Password
DatabaseParams:DataSource **
```

\* You don't actually need a valid SendGrid ApiKey to run this project. Any value will suffice.  
** If using Docker to host SQL Server, set it to `localhost,<port>`; ex: `localhost,1433`.

2 - Apply Database Migrations

Enter the `JwtStore.WebApi` and apply the migrations:

```bash
cd JwtStore.WebApi
dotnet ef database update
```