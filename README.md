# Proyecto Final - Programación I  
## Sistema de Gestión de Hotel (Base de Datos)

![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-blue)
![Status](https://img.shields.io/badge/Estado-En%20Desarrollo-yellow)
![License](https://img.shields.io/badge/Licencia-Uso%20Académico-orange)

---

## Descripción

Este proyecto consiste en el diseño e implementación de una base de datos para la gestión de un hotel.  
Permite administrar información de empleados, usuarios, clientes, habitaciones y reservaciones.

El sistema está orientado a simular el funcionamiento real de un hotel mediante el uso de consultas SQL.

---

## Objetivos

### Objetivo General
Desarrollar una base de datos funcional para la administración de un hotel.

### Objetivos Específicos
- Diseñar tablas con relaciones correctas (PK y FK)
- Insertar datos de prueba
- Realizar consultas SQL relevantes
- Organizar scripts para fácil reutilización

---

## Estructura de la Base de Datos

El sistema incluye las siguientes tablas:

- Empleado  
- Usuario  
- Hotel  
- TipoHabitacion  
- Habitacion  
- Cliente  
- Reservacion  
- DetalleReservacion  

### Relaciones principales:
- Un empleado puede tener un usuario
- Un cliente puede realizar reservaciones
- Una reservación puede incluir varias habitaciones
- Las habitaciones pertenecen a un hotel y a un tipo

---

## Funcionalidades

- Creación de base de datos y tablas  
- Inserción de datos  
- Consultas básicas (`SELECT`)  
- Consultas con `JOIN`  
- Consulta de disponibilidad de habitaciones  

---

## Cómo ejecutar el proyecto

1. Abrir el gestor de base de datos (SQL Server)
2. Ejecutar el archivo:

```sql
DB_Hotel_Script.sql
```

Ejecutar consultas adicionales desde: `queries.txt`

---

## Estado del Proyecto

Actualmente el proyecto se encuentra en desarrollo.  
En esta primera fase se implementó la base de datos.  

Fases pendientes:
- Desarrollo de API
- Interfaz de usuario

---

## Autores

- Nombre: Dario Alfredo Rabe Godoy /Carné: 5190-25-23683
- Nombre: Libbny Dayana Medrano Arévalo /Carné: 5190-25-24096
- Nombre: Richard Esteev Pernillo Macario /Carné: 5190-25-21234
- Nombre: Cristian Daniel Emiliano Cano Estrada /Carné: 5190-25-24608
- Nombre: Diego Jose Quevedo Vega /Carné: 5190-24-21422
- Universidad Mariano Gálvez

## Notas
Este proyecto fue desarrollado con fines académicos como parte del curso de Programación I del tercer semestre de ingeniería en sistemas de la Universidad Mariano Gálvez, impartido por el ingeniero Sebastián Hernández Gabriel

---
