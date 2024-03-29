FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5148

ENV ASPNETCORE_URLS=http://+:5148

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["app/app.csproj", "app/"]
RUN dotnet restore "app/app.csproj"
COPY . .
WORKDIR "/src/app"
RUN dotnet build "app.csproj" -c $configuration -o /app/build

# Run migrations
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="$PATH:/root/.dotnet/tools"
# RUN dotnet-ef database update

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "app.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app.dll"]
