#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal-arm32v7 AS base
RUN apt update && apt install libgpiod-dev gpiod -y
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal-arm32v7 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore -r linux-arm

# Copy everything else and build
COPY . .
RUN dotnet build -c Debug -o /app/build -r linux-arm

FROM build AS publish
RUN dotnet publish -c Debug -o /app/publish -r linux-arm

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "YandexDialogApi.dll"]
