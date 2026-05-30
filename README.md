# Car API

API REST construida con **ASP.NET Core**, **EF Core**, **PostgreSQL**, **XUnit** y **Docker Compose**.

La API expone un endpoint para obtener marcas de autos desde una base de datos PostgreSQL.

## Tecnologías

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Docker Compose
* XUnit
* EF Core InMemory Provider


## Requisitos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

* .NET 8 SDK
* Docker Desktop
* Git

Para verificar la instalación de .NET:

```bash
dotnet --version
```

Para verificar que Docker está corriendo:

```bash
docker version
```

## Base de Datos

El proyecto utiliza PostgreSQL mediante Docker Compose.

Configuración de la base de datos:

```txt
Base de datos: carDb
Usuario: postgres
Contraseña: postgres
Puerto: 5432
```

Dentro de Docker Compose, la API se conecta a PostgreSQL utilizando el nombre del servicio `postgres`.

Connection string utilizada por el contenedor de la API:

```txt
Host=postgres;Port=5432;Database=carDb;Username=postgres;Password=postgres
```

Importante: dentro de Docker Compose, el host de la base de datos es `postgres`, no `localhost`.

## Ejecutar el Proyecto con Docker Compose

Navega a la carpeta del proyecto API donde se encuentra el archivo `docker-compose.yml`:

```bash
cd car.Api
```

Construye e inicia los contenedores:

```bash
docker compose up --build
```

Esto inicia dos servicios:

* `postgres`: base de datos PostgreSQL
* `api`: REST API con ASP.NET Core

La API estará disponible en:

```txt
http://localhost:8080
```

## Endpoint de la API

Obtener todas las marcas de autos:

```http
GET http://localhost:8080/api/MarcasAutos
```

Ejemplo de respuesta:

```json
[
  {
    "id": 1,
    "nombre": "Toyota",
    "pais": "Japan"
  },
  {
    "id": 2,
    "nombre": "Ford",
    "pais": "United States"
  },
  {
    "id": 3,
    "nombre": "BMW",
    "pais": "Germany"
  }
]
```

La base de datos se inicializa con varias marcas de autos cuando se aplican las migraciones.

## Swagger

Swagger está disponible en:

```txt
http://localhost:8080/swagger
```

## Migraciones y Data Seeding

El proyecto incluye una migración de Entity Framework Core que crea la tabla `MarcasAutos`.

La tabla se inicializa con datos de marcas de autos utilizando el mecanismo de seeding de Entity Framework Core.

Cuando la API inicia, las migraciones pendientes se aplican automáticamente desde `Program.cs` usando:

```csharp
dbContext.Database.Migrate();
```

Esto permite que la tabla y los datos iniciales sean creados automáticamente cuando los contenedores de Docker inician.

## Ejecutar Pruebas

Desde la carpeta raíz de la solución:

```bash
dotnet test car.Tests
```

El proyecto de pruebas utiliza:

* XUnit
* Entity Framework Core InMemory database
* Pruebas para el controller
* Pruebas para el service
* Pruebas para el repository

## Cobertura de Pruebas

Para ejecutar las pruebas con cobertura, primero asegúrate de que el proyecto de pruebas tenga instalado Coverlet:

```bash
dotnet add car.Tests package coverlet.collector
```

Luego ejecuta:

```bash
dotnet test car.Tests --collect:"XPlat Code Coverage"
```

Esto genera un reporte de cobertura dentro de:

```txt
car.Tests/TestResults/
```

## Generar Reporte HTML de Cobertura

Instala ReportGenerator:

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```

Luego genera el reporte HTML:

```bash
reportgenerator -reports:"car.Tests/TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html
```

Abre el reporte generado:

```txt
coverage-report/index.html
```

## Validar Cobertura Mínima del 70%

Para validar una cobertura mínima del 70%, instala Coverlet MSBuild:

```bash
dotnet add car.Tests package coverlet.msbuild
```

Luego ejecuta:

```bash
dotnet test car.Tests /p:CollectCoverage=true /p:Threshold=70 /p:ThresholdType=line /p:ThresholdStat=total
```

El comando fallará si la cobertura total de líneas es menor al 70%.

## Comandos Útiles de Docker

Detener los contenedores:

```bash
docker compose down
```

Detener los contenedores y eliminar el volumen de la base de datos:

```bash
docker compose down -v
```

Reconstruir los contenedores:

```bash
docker compose up --build
```

Ver contenedores en ejecución:

```bash
docker ps
```

Ver logs de la API:

```bash
docker compose logs api
```

Ver logs de PostgreSQL:

```bash
docker compose logs postgres
```

## Reiniciar la Base de Datos

Si la base de datos fue creada antes de aplicar las migraciones, puedes reiniciar el volumen de Docker:

```bash
docker compose down -v
docker compose up --build
```

Esto elimina el volumen existente de PostgreSQL y crea la base de datos nuevamente desde cero.

## Patrón de Diseño

El proyecto utiliza una arquitectura simple por capas:

```txt
Controller → Service → Repository → DbContext → PostgreSQL
```

### Controller

Maneja las peticiones y respuestas HTTP.

### Service

Contiene la lógica de aplicación y se encarga de mapear entidades a DTOs.

### Repository

Maneja el acceso a datos utilizando Entity Framework Core.

### DbContext

Configura la conexión a la base de datos, el mapeo de entidades, las migraciones y los datos iniciales.

## Flujo Principal del Endpoint

```txt
GET /api/MarcasAutos
        ↓
MarcasAutosController
        ↓
MarcasAutosService
        ↓
MarcasAutosRepository
        ↓
ApplicationDbContext
        ↓
PostgreSQL
```

## Notas

Si la API devuelve un error indicando que la tabla `MarcasAutos` no existe, verifica lo siguiente:

1. La migración existe dentro de la carpeta `Migrations`.
2. La API está ejecutando `dbContext.Database.Migrate()` al iniciar.
3. El volumen de PostgreSQL fue reiniciado si era necesario:

```bash
docker compose down -v
docker compose up --build
```

## Resumen

Este proyecto incluye:

* Conexión a PostgreSQL usando Entity Framework Core
* Migración para crear la tabla `MarcasAutos`
* Data seeding con ejemplos de marcas de autos
* Endpoint REST para obtener marcas de autos
* Capas Repository y Service
* Pruebas unitarias con XUnit
* Pruebas usando base de datos en memoria
* Configuración de Docker Compose para PostgreSQL y la REST API
