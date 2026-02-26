# GtMotive Sergio Rojas Test

## Cómo ejecutar

### Sin Docker

```bash
cd src/GtMotive.Estimate.Microservice.Host
dotnet run
```

Se abre en `https://localhost:{puerto}/swagger`.

### Con Docker

```bash
docker compose up --build (En la carpeta raiz del proyecto, donde se encuntra el Dockerfile)
```

Acceder a `http://localhost:5000/swagger`.

### Tests

```bash
dotnet test
```

O desde VS

---

## Decisiones de arquitectura

### ¿Por qué no he creado una entidad Customer?

El enunciado no lo pide. Las 4 acciones son sobre vehículos y alquileres. El cliente solo aparece como una restricción ("una misma persona no puede reservar más de 1 vehículo"), no como una entidad a gestionar. No hay CRUD de customer por decirlo de alguna forma.

CustomerIdentifier es un identificador externo (un DNI por ejemplo) que se usa para verificar la restricción. En un sistema real, el cliente viviría en otro bounded context (otro microservicio) y este servicio de renting solo recibiría su identificador.

### ¿Por qué las interfaces de repositorio están en Domain y no en ApplicationCore?

Las dos opciones son válidas. En Clean Architecture pura de Uncle Bob, irían en Application porque son los use cases los que las necesitan. En DDD, van en Domain porque modelan cómo se persisten los aggregate roots.

Elegí Domain por coherencia con la propia template.

### ¿Por qué no uso Value Objects?

El README de la template menciona Value Objects como patrón DDD. No los he implementado por el alcance de la prueba. Si el dominio creciera (por ejemplo matrñiculas con formato específico por país), entonces tendría sentido extraerlos.

### ¿Por qué no uso Unit of Work?

La interfaz `IUnitOfWork` existe en la template, pero como uso repositorios InMemory con `ConcurrentDictionary`, no hay transacciones que coordinar. La arquitectura permite migrar a Entity Framework sin tocar los use cases gracias a la abstracción.

---

## Problemas que he ido resolviendo

### global.json

La template venía con una versión exacta del SDK en `global.json` que no coincidía con la instalada. Añadí `"rollForward": "latestMajor"` para que acepte cualquier versión compatible del SDK 9.x.

### Dependencias de test

En el `Directory.Build.targets` fijaba `Microsoft.AspNetCore.TestHost`, `Microsoft.Extensions.Hosting` y `Microsoft.Extensions.Configuration.EnvironmentVariables` en versión 6.0.0. Esto causaba un error en los tests de infraestructura, un bug conocido de incompatibilidad entre TestHost 6.x y .NET 9. La solución fue actualizar esos 3 paquetes a 9.0.0.

### Visibilidad de clases en tests de infraestructura (internal → public)

La template tenía `GenericInfrastructureTestServerFixture`, `InfrastructureTestBase` y `TestServerCollectionFixture` como internal. Al crear los tests, xUnit requiere que las clases de test sean public. Cambié las 3 clases auxiliares a public.

En los tests funcionales tuve un problema similar y resuelto de la misma forma.

### Supresión de warnings de documentación en tests

StyleCop exige XML docs en todos los miembros públicos. Añadí `<NoWarn>$(NoWarn);SA1600;SA1601;CS1591;CA1515</NoWarn>` en los 3 proyectos de test.

### Value types en Requests y el warning S6964

SonarQube avisa de que DateTime y Guid en propiedades de un controller action pueden causar under-posting: si el cliente no envía el campo, en vez de dar error se queda con la fecha por defecto o Guid.Empty. El [Required] no funciona con value types porque nunca son null.

La solución fue hacerlos nullable.

### ArgumentNullException.ThrowIfNull en controllers

El análisis de código pide validar parámetros en métodos públicos. Usé `ArgumentNullException.ThrowIfNull(request)` al inicio de cada action del controller.

---

## Flujo de una petición (ejemplo de CreateVehicle)

```
POST /api/vehicles (JSON)
       │
       ▼
  CreateVehicleRequest          ← DTO de entrada HTTP
       │
       ▼
  VehiclesController            ← recibe y delega a MediatR
       │
       ▼
  CreateVehicleRequestHandler   ← Request convertimos a Input y llama al caso de uso
       │
       ▼
  CreateVehicleInput            ← DTO de negocio
       │
       ▼
  CreateVehicleUseCase          ← lógica de negocio
       │
       ▼
  CreateVehicleOutput           ← resultado
       │
       ▼
  CreateVehiclePresenter        ← traduce Output → Response + ActionResult (código 201)
       │
       ▼
  CreateVehicleResponse         ← DTO de salida HTTP
       │
       ▼
  HTTP 201 Created + JSON
```

¿Por qué tantas clases? Cada una vive en una capa distinta y puede cambiar independientemente. Si mañana cambio REST por un consumer de RabbitMQ (por poner un ejemplo), solo toco Request/Response/Controller. Si cambio la persistencia en memoria por SQL Server, solo toco Infrastructure. Los use cases no se enteran de nada.

---

## Patterns utilizados (del README de la template)

| Pattern | Dónde |
|---------|-------|
| Controller + Command (MediatR) | Controllers delegan a handlers |
| ViewModel / Response | `*Response.cs` como DTOs de salida HTTP |
| Presenter | `*Presenter.cs` |
| Repository | `IVehicleRepository`, `IRentalRepository` (Domain) → `InMemory*` (Infrastructure) |
| Entity | `Vehicle`, `Rental` con validaciones|
| Use Case | 4 use cases en ApplicationCore |
| Separation of Concerns | Domain → ApplicationCore → Infrastructure → Api → Host |

---

## Estructura de tests

| Tipo | Proyecto | Qué testea | Ejemplo |
|------|----------|-------------|---------|
| Unitario | UnitTests | Entidades y use cases aislados | VehicleTests, CreateVehicleUseCaseTests |
| Funcional | FunctionalTests | Flujo completo con dependencias reales, sin servidor HTTP | RentalWorkflowTests |
| Infraestructura | InfrastructureTests | Endpoints HTTP reales usando TestServer | VehiclesControllerTests |

---

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/vehicles` | Crear vehículo |
| GET | `/api/vehicles/available` | Traer vehículos disponibles |
| POST | `/api/rentals` | Alquilar un vehículo |
| POST | `/api/rentals/{rentalId}/return` | Devolver un vehículo |

Esta Swagger configurado

## Reglas de negocio

- Un vehículo no puede tener más de 5 años de antigüedad.
- Una persona no puede tener más de 1 alquiler activo simultáneamente.
- Un vehículo no disponible no se puede alquilar.
- Un alquiler ya completado no se puede completar de nuevo.