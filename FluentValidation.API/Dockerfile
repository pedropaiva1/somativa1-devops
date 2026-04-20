FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FluentValidation.API/FluentValidation.API.csproj", "FluentValidation.API/"]
RUN dotnet restore "FluentValidation.API/FluentValidation.API.csproj"
COPY . .
WORKDIR "/src/FluentValidation.API"
RUN dotnet build "FluentValidation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluentValidation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluentValidation.API.dll"]