FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY LocalisationSheet.Server.csproj ./
RUN dotnet restore "LocalisationSheet.Server.csproj"
COPY . .
RUN dotnet publish "LocalisationSheet.Server.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "LocalisationSheet.Server.dll"]
