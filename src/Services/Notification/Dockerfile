FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app/miau/notification

EXPOSE 5280

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app/miau/notification
COPY ["./Services/Notification/Notification.WebApi/Notification.WebApi.csproj", "./Services/Notification/Notification.WebApi/Notification.WebApi.csproj"]
COPY ["./BuildingBlocks/EventBus/EventBus.csproj", "./BuildingBlocks/EventBus/EventBus.csproj"]
COPY ["./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj"]
RUN dotnet restore "./Services/Notification/Notification.WebApi/Notification.WebApi.csproj"
RUN dotnet restore "./BuildingBlocks/EventBus/EventBus.csproj"
RUN dotnet restore "./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj"

COPY . /app/miau/notification
WORKDIR "/app/miau/notification/."
RUN dotnet build "./Services/Notification/Notification.WebApi/Notification.WebApi.csproj" -c Release -o /app/miau/notification/build
RUN dotnet build "./BuildingBlocks/EventBus/EventBus.csproj" -c Release -o /app/miau/notification/build
RUN dotnet build "./BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj" -c Release -o /app/miau/notification/build

FROM build AS publish
RUN dotnet publish "./Services/Notification/Notification.WebApi/Notification.WebApi.csproj" -c Release -o /app/miau/notification/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/miau/notification
COPY --from=publish /app/miau/notification/publish .

ENTRYPOINT ["dotnet", "Notification.WebApi.dll"]