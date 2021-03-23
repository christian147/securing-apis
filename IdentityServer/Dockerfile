FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS base
ARG IdentityServerPort
ARG BuildConfiguration
RUN echo "Current BuildConfiguration: $BuildConfiguration"
RUN echo "Current IdentityServerPort: $IdentityServerPort"

FROM base AS build
ARG BuildConfiguration
WORKDIR /build
COPY IdentityServer .
RUN dotnet build IdentityServer.sln --configuration $BuildConfiguration

FROM build AS publish
ARG BuildConfiguration
WORKDIR /build
RUN dotnet publish IdentityServer.sln --configuration $BuildConfiguration --output /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
ARG IdentityServerPort
EXPOSE $IdentityServerPort
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]