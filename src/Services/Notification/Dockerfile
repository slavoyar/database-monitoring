FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app/miau/notification

EXPOSE 5280

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app/miau/notification
COPY ["Notification.WebApi/Notification.WebApi.csproj", "Notification.WebApi/"]
RUN dotnet restore "Notification.WebApi/Notification.WebApi.csproj"
COPY . .
WORKDIR "/app/miau/notification/Notification.WebApi"
RUN dotnet build "Notification.WebApi.csproj" -c Release -o /app/miau/notification/build

FROM build AS publish
RUN dotnet publish "Notification.WebApi.csproj" -c Release -o /app/miau/notification/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/miau/notification
COPY --from=publish /app/miau/notification/publish .


ENTRYPOINT ["dotnet", "Notification.WebApi.dll"]