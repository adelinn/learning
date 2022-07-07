# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Application/*.csproj ./Application/
COPY Tests/*.csproj ./Tests/
RUN dotnet restore

# Copy everything else and build
COPY Application/. ./Application/
WORKDIR /source/Application
RUN dotnet publish -c release -o /app --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app ./
EXPOSE $PORT 443
ENTRYPOINT ["dotnet", "dotNetLearn.dll"]