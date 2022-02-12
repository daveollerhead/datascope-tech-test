FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DatascopeTests.IntegrationTests/DatascopeTests.IntegrationTests.csproj", "DatascopeTests.IntegrationTests/"]
COPY ["DatascopeTest/DatascopeTest.csproj", "DatascopeTest/"]
RUN dotnet restore "DatascopeTests.IntegrationTests/DatascopeTests.IntegrationTests.csproj" --disable-parallel

COPY . .
WORKDIR "/src/DatascopeTests.IntegrationTests"
# RUN dotnet build "DatascopeTests.IntegrationTests.csproj"
CMD ["dotnet", "test"]
