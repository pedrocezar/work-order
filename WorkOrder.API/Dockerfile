FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorkOrder.API/WorkOrder.API.csproj", "WorkOrder.API/"]
COPY ["WorkOrder.Domain/WorkOrder.Domain.csproj", "WorkOrder.Domain/"]
COPY ["WorkOrder.Infrastructure/WorkOrder.Infrastructure.csproj", "WorkOrder.Infrastructure/"]
RUN dotnet restore "./WorkOrder.API/./WorkOrder.API.csproj"
COPY . .
WORKDIR "/src/WorkOrder.API"
RUN dotnet build "./WorkOrder.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkOrder.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkOrder.API.dll"]