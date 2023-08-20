FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5255

ENV ASPNETCORE_URLS=http://+:5255

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BrezyWeather/BrezyWeather.csproj", "./"]
RUN dotnet restore "BrezyWeather.csproj"
COPY BrezyWeather/. .
WORKDIR "/src/."
RUN dotnet build "BrezyWeather.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrezyWeather.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrezyWeather.dll"]
