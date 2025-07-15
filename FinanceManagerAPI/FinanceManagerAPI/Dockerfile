#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinanceManagerAPI/FinanceManager.WebApi.csproj", "FinanceManagerAPI/"]
COPY ["FinanceManager.Domain/FinanceManager.Domain.csproj", "FinanceManager.Domain/"]
COPY ["FinanceManager.Infrastructure/FinanceManager.Infrastructure.csproj", "FinanceManager.Infrastructure/"]
RUN dotnet restore "./FinanceManagerAPI/./FinanceManager.WebApi.csproj"
COPY . .
WORKDIR "/src/FinanceManagerAPI"
RUN dotnet build "./FinanceManager.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FinanceManager.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceManager.WebApi.dll"]