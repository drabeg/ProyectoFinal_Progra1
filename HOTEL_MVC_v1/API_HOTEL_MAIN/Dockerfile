# 1. Capa de compilación (SDK de .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos todo el contenido del repositorio
COPY . .

# Restauramos y publicamos apuntando directamente al .csproj de la API
RUN dotnet restore "HOTEL_MVC_v1/API_HOTEL_MAIN/API_HOTEL_MAIN.csproj"
RUN dotnet publish "HOTEL_MVC_v1/API_HOTEL_MAIN/API_HOTEL_MAIN.csproj" -c Release -o /app/publish

# 2. Capa de ejecución (Runtime de .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto por defecto para la API
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "API_HOTEL_MAIN.dll"]