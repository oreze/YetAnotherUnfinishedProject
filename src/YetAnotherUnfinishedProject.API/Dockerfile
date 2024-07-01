FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

COPY ["YetAnotherUnfinishedProject.API.csproj", "YetAnotherUnfinishedProject.API/"]

RUN dotnet restore "YetAnotherUnfinishedProject.API/YetAnotherUnfinishedProject.API.csproj"
WORKDIR /src
COPY . .
RUN ls -la
RUN dotnet build "YetAnotherUnfinishedProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "YetAnotherUnfinishedProject.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YetAnotherUnfinishedProject.API.dll"]
