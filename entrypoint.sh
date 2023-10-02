#!/bin/bash

dotnet tool restore
dotnet ef database update

dotnet JwtStore.WebApi.dll