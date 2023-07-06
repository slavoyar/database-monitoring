FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app/miau/agregation

EXPOSE 5278

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app/miau/agregation
COPY ["Agregation.csproj", "."]
RUN dotnet restore "./Agregation.csproj"
COPY . /app/miau/agregation
WORKDIR "/app/miau/agregation/."
RUN dotnet build "Agregation.csproj" -c Release -o /app/miau/agregation/build

FROM build AS publish
RUN dotnet publish "Agregation.csproj" -c Release -o /app/miau/agregation/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/miau/agregation
COPY --from=publish /app/miau/agregation/publish .
ENTRYPOINT ["dotnet", "Agregation.dll"]