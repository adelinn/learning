FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app
COPY . .

RUN dotnet restore

# run tests on docker run
ENTRYPOINT ["dotnet", "test"]