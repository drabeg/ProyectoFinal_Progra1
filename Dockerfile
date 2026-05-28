# 1. Capa de compilación (SDK de .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos todo el contenido del repositorio
COPY . .

# Restauramos dependencias apuntando a la ruta exacta descubierta
RUN dotnet restore "HOTEL_MVC_v1/FRONT_HOTEL_MVC/HotelMVC/HotelMVC.csproj"

# Compilamos y publicamos el proyecto Web directo a la carpeta /app/publish
RUN dotnet publish "HOTEL_MVC_v1/FRONT_HOTEL_MVC/HotelMVC/HotelMVC.csproj" -c Release -o /app/publish

# 2. Capa de ejecución (Runtime de .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Forzar el puerto 80 para que Railway asigne tu dominio público
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# El ejecutable real de salida de tu proyecto según el .csproj
ENTRYPOINT ["dotnet", "HotelMVC.dll"]