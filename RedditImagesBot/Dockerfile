#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RedditImagesBot/RedditImagesBot.csproj", "RedditImagesBot/"]
RUN dotnet restore "RedditImagesBot/RedditImagesBot.csproj"
COPY . .
WORKDIR "/src/RedditImagesBot"
RUN dotnet build "RedditImagesBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedditImagesBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedditImagesBot.dll"]