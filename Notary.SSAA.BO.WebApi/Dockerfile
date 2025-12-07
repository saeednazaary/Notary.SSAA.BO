# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Notary.SSAA.BO.WebApi/Notary.SSAA.BO.WebApi.csproj", "Notary.SSAA.BO.WebApi/"]
COPY ["Notary.SSAA.BO.Application.UnitTests/Notary.SSAA.BO.Application.UnitTests.csproj", "Notary.SSAA.BO.Application.UnitTests/"]
COPY ["Notary.SSAA.BO.CommandHandler/Notary.SSAA.BO.CommandHandler.csproj", "Notary.SSAA.BO.CommandHandler/"]
COPY ["Notary.SSAA.BO.Coordinator/Notary.SSAA.BO.Coordinator.csproj", "Notary.SSAA.BO.Coordinator/"]
COPY ["Notary.SSAA.BO.Configuration/Notary.SSAA.BO.Configuration.csproj", "Notary.SSAA.BO.Configuration/"]
COPY ["Notary.SSAA.BO.DataTransferObject/Notary.SSAA.BO.DataTransferObject.csproj", "Notary.SSAA.BO.DataTransferObject/"]
COPY ["Notary.SSAA.BO.Domain/Notary.SSAA.BO.Domain.csproj", "Notary.SSAA.BO.Domain/"]
COPY ["Notary.SSAA.BO.SharedKernel/Notary.SSAA.BO.SharedKernel.csproj", "Notary.SSAA.BO.SharedKernel/"]
COPY ["Notary.SSAA.BO.Utilities/Notary.SSAA.BO.Utilities.csproj", "Notary.SSAA.BO.Utilities/"]
COPY ["Notary.SSAA.BO.Infrastructure/Notary.SSAA.BO.Infrastructure.csproj", "Notary.SSAA.BO.Infrastructure/"]
COPY ["Notary.SSAA.BO.QueryHandler/Notary.SSAA.BO.QueryHandler.csproj", "Notary.SSAA.BO.QueryHandler/"]
COPY ["Notary.SSAA.BO.ExternalServices/Notary.SSAA.BO.ServiceHandler.csproj", "Notary.SSAA.BO.ExternalServices/"]
RUN dotnet restore "./Notary.SSAA.BO.WebApi/Notary.SSAA.BO.WebApi.csproj"
COPY . .
WORKDIR "/src/Notary.SSAA.BO.WebApi"
RUN dotnet build "./Notary.SSAA.BO.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Notary.SSAA.BO.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notary.SSAA.BO.WebApi.dll"]