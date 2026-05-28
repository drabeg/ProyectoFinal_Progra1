# 1. Capa de compilación (SDK de .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos todo el contenido del repositorio al contenedor
COPY . .

# Ejecutamos el restore apuntando directamente a tu archivo .slnx
RUN dotnet restore "HOTEL_MVC_v1/HotelMVCVISUAL.slnx"

# Compilamos y publicamos el proyecto usando la ruta exacta de la solución
RUN dotnet publish "HOTEL_MVC_v1/HotelMVCVISUAL.slnx" -c Release -o /app/publish

# 2. Capa de ejecución (Runtime de .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Forzar el puerto 80 que es el estándar que Railway mapea hacia el exterior
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Ejecutable principal de la aplicación al arrancar
ENTRYPOINT ["dotnet", "FRONT_MVC_HOTEL.dll"]