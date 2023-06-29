FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app/miau/workspace
EXPOSE 5165

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app/miau/workspace
COPY ["./Services/Workspace/Workspace.csproj", "./Services/Workspace/Workspace.csproj"]
COPY ["./BuildingBlocks/EventBus/EventBus.csproj", "./BuildingBlocks/EventBus/EventBus.csproj"]
COPY ["./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj"]
RUN dotnet restore "./Services/Workspace/Workspace.csproj"
RUN dotnet restore "./BuildingBlocks/EventBus/EventBus.csproj"
RUN dotnet restore "./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj"

COPY . /app/miau/workspace
WORKDIR "/app/miau/workspace/."
RUN dotnet build "./Services/Workspace/Workspace.csproj" -c Release -o /app/miau/workspace/build
RUN dotnet build "./BuildingBlocks/EventBus/EventBus.csproj" -c Release -o /app/miau/workspace/build
RUN dotnet build "./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj" -c Release -o /app/miau/workspace/build

FROM build AS publish
RUN dotnet publish "./Services/Workspace/Workspace.csproj" -c Release -o /app/miau/workspace/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/miau/workspace
COPY --from=publish /app/miau/workspace/publish .
ENTRYPOINT ["dotnet", "Workspace.dll"]