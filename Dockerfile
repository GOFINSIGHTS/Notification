# Stage 1: Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV ASPNETCORE_ENVIRONMENT=Production

# Stage 2: Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Notification/Notification.csproj", "Notification/"]
COPY ["Notification.Domain/Notification.Domain.csproj", "Notification.Domain/"]
COPY ["Notification.Infrastructure.Implementation/Notification.Infrastructure.Implementation.csproj", "Notification.Infrastructure.Implementation/"]
COPY ["Notification.Service.Abstractions/Notification.Service.Abstractions.csproj", "Notification.Service.Abstractions/"]
COPY ["Notification.Infrastructure.PostgreSql/Notification.Infrastructure.PostgreSql.csproj", "Notification.Infrastructure.PostgreSql/"]
COPY ["Notification.Infrastructure/Notification.Infrastructure.csproj", "Notification.Infrastructure/"]
COPY ["Notification.Service/Notification.Service.csproj", "Notification.Service/"]

RUN dotnet restore "Notification/Notification.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Notification"
RUN dotnet build "Notification.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notification.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.dll"]