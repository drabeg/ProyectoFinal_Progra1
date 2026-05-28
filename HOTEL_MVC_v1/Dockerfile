# 1. Capa de compilación (SDK de .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de la solución y restaurar
COPY *.sln ./
COPY . .
RUN dotnet restore

# Compilar el proyecto en modo Release
RUN dotnet publish -c Release -o /app/publish

# 2. Capa de ejecución (Runtime de .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Railway requiere mapear el puerto dinámicamente o usar el 80
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Reemplaza "FRONT_MVC_HOTEL.dll" si el ejecutable principal se llamara distinto
ENTRYPOINT ["dotnet", "FRONT_MVC_HOTEL.dll"]