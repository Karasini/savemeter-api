#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Api/Instapp.Services.Finances.Api.csproj", "Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Api/"]
COPY ["Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Application/Instapp.Services.Finances.Application.csproj", "Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Application/"]
COPY ["Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Domain/Instapp.Services.Finances.Domain.csproj", "Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Domain/"]
COPY ["Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Infrastructure/Instapp.Services.Finances.Infrastructure.csproj", "Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Infrastructure/"]
COPY ["Instapp.Common/Instapp.Common.Application/Instapp.Common.Application.csproj", "Instapp.Common/Instapp.Common.Application/"]
COPY ["Instapp.Common/Instapp.Common.WebApi/Instapp.Common.WebApi.csproj", "Instapp.Common/Instapp.Common.WebApi/"]
COPY ["Instapp.Common/Instapp.Common.Logger/Instapp.Common.Logger.csproj", "Instapp.Common/Instapp.Common.Logger/"]
COPY ["Instapp.Common/Instapp.Common.Exception/Instapp.Common.Exception.csproj", "Instapp.Common/Instapp.Common.Exception/"]
COPY ["Instapp.Common/Instapp.Common.Cqrs/Instapp.Common.Cqrs.csproj", "Instapp.Common/Instapp.Common.Cqrs/"]
COPY ["Instapp.Common/Instapp.Common.Domain/Instapp.Common.Domain.csproj", "Instapp.Common/Instapp.Common.Domain/"]
COPY ["Instapp.Common/Instapp.Common.Infrastructure/Instapp.Common.Infrastructure.csproj", "Instapp.Common/Instapp.Common.Infrastructure/"]
RUN dotnet restore "Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Api/Instapp.Services.Finances.Api.csproj"
COPY . .
WORKDIR "/src/Instapp.Services/Instapp.Services.Finances/src/Instapp.Services.Finances.Api"
RUN dotnet build "Instapp.Services.Finances.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Instapp.Services.Finances.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5004
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Instapp.Services.Finances.Api.dll"]
