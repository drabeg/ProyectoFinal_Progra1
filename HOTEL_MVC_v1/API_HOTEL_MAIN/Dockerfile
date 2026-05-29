# 1. Capa de compilación (SDK de .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo el contenido del repositorio para compilar
COPY . .

# Restauramos y publicamos buscando directamente el archivo .csproj
RUN dotnet restore "API_HOTEL_MAIN.csproj"
RUN dotnet publish "API_HOTEL_MAIN.csproj" -c Release -o /app/publish

# 2. Capa de ejecución (Runtime de .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto por defecto para la API
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "API_HOTEL_MAIN.dll"]