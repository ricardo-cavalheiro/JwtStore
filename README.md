## JwtStore.WebApi

This project uses `dotnet user-secrets` to store some env variables. The following is the list of env varibles you should set using `dotnet user-secrets`:

```
SendGrid:ApiKey
Secrets:PasswordSaltKey
Secrets:JwtPrivateKey
Secrets:ApiKey
Email:DefaultFromName
Email:DefaultFromEmail
DatabaseParams:Password
```

PS: you don't actually need a valid SendGrid ApiKey to run this project. Any value will suffice.