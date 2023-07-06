FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app/miau/auth
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app/miau/auth
COPY ["Auth.csproj", "."]
RUN dotnet restore "./Auth.csproj"
COPY . /app/miau/auth
WORKDIR "/app/miau/auth/."
RUN dotnet build "Auth.csproj" -c Release -o /app/miau/auth/build

FROM build AS publish
RUN dotnet publish "Auth.csproj" -c Release -o /app/miau/auth/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/miau/auth
COPY --from=publish /app/miau/auth/publish .
ENTRYPOINT ["dotnet", "Auth.dll"]