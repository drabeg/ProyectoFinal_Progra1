# 1. Capa de compilación (SDK de .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo el contenido del repositorio al contenedor
COPY . .

# Nos movemos a la subcarpeta donde está el código real del hotel
WORKDIR /src/HOTEL_MVC_v1

# Restauramos las dependencias usando el nuevo formato .slnx
RUN dotnet restore

# Compilamos y publicamos el proyecto en modo Release
RUN dotnet publish -c Release -o /app/publish

# 2. Capa de ejecución (Runtime de .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Forzar el puerto 80 para que Railway conecte el tráfico web
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Ejecutable principal
ENTRYPOINT ["dotnet", "FRONT_MVC_HOTEL.dll"]