FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Services/TestPatient/TestPatient.csproj", "./Services/TestPatient/TestPatient.csproj"]
RUN dotnet restore "./Services/TestPatient/TestPatient.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Services/TestPatient/TestPatient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Services/TestPatient/TestPatient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestPatient.dll"]
