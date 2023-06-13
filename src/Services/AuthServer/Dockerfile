FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /usr/src/miau/auth

COPY . ./
COPY *.sln .
COPY *.csproj ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /usr/src/miau/auth
COPY --from=build /usr/src/miau/auth/out ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Auth.dll"]