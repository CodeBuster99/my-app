FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /source

COPY ./src/Host/*.csproj ./src/Host/
COPY ./src/Application/*.csproj ./src/Application/
COPY ./src/Modules/SavedMessage/*.csproj ./src/Modules/SavedMessage/

RUN dotnet restore ./src/Host/Host.csproj

COPY ./src ./src

RUN dotnet publish ./src/Host/Host.csproj \
    -c Release \
    -o /app \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app .

# to hold sqlite app.db
RUN mkdir -p /app/data

EXPOSE 8080

ENTRYPOINT ["dotnet", "Host.dll"]
