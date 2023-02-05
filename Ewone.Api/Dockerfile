FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ewone.Api/Ewone.Api.csproj", "Ewone.Api/"]
RUN dotnet restore "Ewone.Api/Ewone.Api.csproj"
COPY . .
WORKDIR "/src/Ewone.Api"
RUN dotnet build "Ewone.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ewone.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ewone.Api.dll"]
