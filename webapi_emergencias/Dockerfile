FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

# Crear carpeta en runtime
RUN mkdir -p /app/Imagenes

ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 10000
CMD ["dotnet", "webapi_emergencias.dll"]
