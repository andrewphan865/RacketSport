#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Catalog/Catalog.Api/Catalog.Api.csproj", "src/Services/Catalog/Catalog.Api/"]
COPY ["src/Services/Basket/Basket.Api/Basket.Api.csproj", "src/Services/Basket/Basket.Api/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/BuildingBlocks/Healthchecks/Healthchecks.csproj", "src/BuildingBlocks/Healthchecks/"]

RUN dotnet restore "src/Services/Basket/Basket.Api/Basket.Api.csproj"

COPY . .
WORKDIR "/src/src/Services/Basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-restore "Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]