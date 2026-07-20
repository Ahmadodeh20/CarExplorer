# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["CarExplorer/CarExplorer.csproj", "CarExplorer/"]

RUN dotnet restore "CarExplorer/CarExplorer.csproj"

COPY . .

WORKDIR "/src/CarExplorer"

RUN dotnet build "CarExplorer.csproj" -c Release -o /app/build


# Publish stage
FROM build AS publish

RUN dotnet publish "CarExplorer.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CarExplorer.dll"]