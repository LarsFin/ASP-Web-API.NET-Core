FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY NET-Core-ASP-API/NET-Core-ASP-API.csproj NET-Core-ASP-API/
RUN dotnet restore "NET-Core-ASP-API/NET-Core-ASP-API.csproj"
COPY . .
WORKDIR "/src/NET-Core-ASP-API"
RUN dotnet build "NET-Core-ASP-API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NET-Core-ASP-API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NET-Core-ASP-API.dll"]
