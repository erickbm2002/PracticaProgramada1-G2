# Práctica Programada 1 - Sistema Gestor de Clientes

**Universidad Fidélitas**  
**Curso:** Programación Avanzada (SC-701)  
**Profesor:** Richard Arce Vargas  

---

## 1. Integrantes del Grupo

| Nombre Completo | Correo Institucional |
| :--- | :--- |
| Erick Alejandro Barrientos Montes | ebarrientos80448@ufide.ac.cr |
| Clark Delenny Samuels Gonzalez | csamuels00989@ufide.ac.cr |
| Edwin Camilo Pardo Garcia |epardo32120@ufide.ac.cr |
| Steven Mauricio Fonseca Herrera | sfonseca10995@ufide.ac.cr |

---

## 2. Repositorio

* **URL del Repositorio:** [https://github.com/usuario/PracticaProgramada1G2](https://github.com/usuario/PracticaProgramada1G2)

---

## 3. Especificación Técnica de la Solución

### A. Arquitectura del Proyecto
El proyecto está estructurado bajo una arquitectura multicapa (Clean Architecture / N-Capas) en .NET:

* **`PracticaProgramada1G2` (Presentación / Web API):** Contiene los controladores REST (`Controllers`), configuración de middlewares, endpoints e inyección de dependencias de la aplicación.
* **`PracticaProgramada1G2.Application` (Lógica de Aplicación):** Biblioteca de clases (.NET Class Library) que contiene casos de uso, servicios (`Services`), contratos/interfaces (`Interfaces`), DTOs y validaciones de negocio.
* **`PracticaProgramada1G2.Domain` (Capa de Dominio):** Biblioteca de clases (.NET Class Library) que alberga las entidades del modelo de negocio (`Cliente`, `Telefono`), excepciones de dominio y contratos esenciales sin dependencias externas.
* **`PracticaProgramada1G2.Infraestructure` (Acceso a Datos e Infraestructura):** Biblioteca de clases (.NET Class Library) encargada de la persistencia de datos, configuración de `DbContext`, implementaciones de repositorios (`Repositories`) y migraciones de base de datos.

---

### B. Librerías y Paquetes NuGet Utilizados

* **`Microsoft.EntityFrameworkCore`**: ORM principal para el mapeo objeto-relacional.
* **`Microsoft.EntityFrameworkCore.SqlServer`** (o `Pomelo.EntityFrameworkCore.MySql` / `Npgsql`): Proveedor de base de datos para Entity Framework Core.
* **`Microsoft.EntityFrameworkCore.Tools`**: Herramientas para gestión de migraciones en consola.
* **`Swashbuckle.AspNetCore`**: Generación de documentación interactiva OpenAPI/Swagger para los endpoints.
* **`AutoMapper.Extensions.Microsoft.DependencyInjection`** *(opcional)*: Simplificación del mapeo entre entidades de dominio y DTOs.
* **`FluentValidation.AspNetCore`** *(opcional)*: Validación declarativa de modelos de entrada.

---

### C. Principios SOLID y Patrones de Diseño Utilizados

#### Principios SOLID
* **S (Single Responsibility Principle):** Cada capa y clase tiene una responsabilidad única. Por ejemplo, los controladores solo manejan HTTP, los servicios coordinan lógica de aplicación y los repositorios encapsulan consultas a la base de datos.
* **O (Open/Closed Principle):** Se definen interfaces que permiten extender la funcionalidad (como nuevos proveedores de almacenamiento o validadores) sin modificar el código consumidor.
* **L (Liskov Substitution Principle):** Las implementaciones concretas de interfaces de repositorio o servicios pueden sustituirse sin alterar el correcto funcionamiento del sistema.
* **I (Interface Segregation Principle):** Interfaces segregadas y específicas (`IClienteRepository`, `IClienteService`) evitando que las clases dependan de métodos que no utilizan.
* **D (Dependency Inversion Principle):** Las capas de alto nivel (`Application`, `Web API`) dependen de abstracciones (interfaces en `Domain` o `Application`), no de implementaciones concretas de `Infraestructure`. Las dependencias se resuelven mediante el contenedor IoC nativo de .NET.

#### Patrones de Diseño
* **Repository Pattern:** Desacopla la lógica de negocio del mecanismo de persistencia mediante abstracciones como `IClienteRepository`.
* **Data Transfer Object (DTO):** Aísla el modelo de dominio de la capa externa, exponiendo únicamente la información necesaria y evitando dependencias circulares al serializar relaciones.
* **Dependency Injection (Inyección de Dependencias):** Utilizado de forma transversal en la solución a través del contenedor nativo de ASP.NET Core (`Program.cs`).


## 4. Requerimientos Funcionales y Flujo en las Capas[cite: 1, 3]

A continuación se detalla la traza completa de cada requerimiento del departamento de cartera a través de las capas de la solución[cite: 3, 5]:

### RF-01: Listado de Clientes
* **Descripción:** Obtiene la lista completa de clientes registrados junto con sus números de teléfono asociados[cite: 3].
* **Flujo entre Capas:**
  1. **`PracticaProgramada1G2` (Presentación):** `ClientesController` recibe la petición `GET /api/clientes` y delega la ejecución al servicio[cite: 5].
  2. **`PracticaProgramada1G2.Application`:** `ClienteService.ObtenerTodosAsync()` llama a la abstracción `IClienteRepository`[cite: 5].
  3. **`PracticaProgramada1G2.Infraestructure`:** `ClienteRepository` consulta la base de datos mediante `DbContext`, cargando las entidades `Cliente` e incluyendo sus teléfonos relacionados (`Include(c => c.Telefonos)`)[cite: 3, 5].
  4. **`PracticaProgramada1G2.Domain`:** Las entidades `Cliente` y `Telefono` aportan su estructura de datos[cite: 3, 5].
  5. **`PracticaProgramada1G2.Application`:** Se mapea la lista de entidades a `ClienteDto`[cite: 5].
  6. **`PracticaProgramada1G2` (Presentación):** Retorna código `200 OK` con la colección de datos al usuario[cite: 5].

---

### RF-02: Ver Detalle de un Cliente
* **Descripción:** Obtiene la información completa de un cliente específico mediante su identificador[cite: 3].
* **Flujo entre Capas:**
  1. **`PracticaProgramada1G2` (Presentación):** `ClientesController` recibe `GET /api/clientes/{id}`[cite: 5].
  2. **`PracticaProgramada1G2.Application`:** `ClienteService.ObtenerPorIdAsync(id)` valida el parámetro y solicita la entidad al repositorio[cite: 5].
  3. **`PracticaProgramada1G2.Infraestructure`:** `ClienteRepository` consulta por ID incluyendo la relación con sus teléfonos[cite: 3, 5].
  4. **`PracticaProgramada1G2.Application`:** Si no se encuentra el registro, retorna nulo/not found; si existe, transforma la entidad a `ClienteDetalleDto`[cite: 5].
  5. **`PracticaProgramada1G2` (Presentación):** Devuelve `200 OK` con el cliente o `404 Not Found`[cite: 5].

---

### RF-03: Registrar un Cliente
* **Descripción:** Registra un nuevo cliente junto con sus números de teléfono en el sistema[cite: 3].
* **Flujo entre Capas:**
  1. **`PracticaProgramada1G2` (Presentación):** `ClientesController` recibe `POST /api/clientes` con el cuerpo `CrearClienteDto`[cite: 5].
  2. **`PracticaProgramada1G2.Application`:** `ClienteService.CrearAsync(dto)` valida la información y mapea los datos a nuevas instancias de las entidades `Cliente` y `Telefono` del Dominio[cite: 5].
  3. **`PracticaProgramada1G2.Domain`:** Se aplican reglas y validaciones de modelo sobre los atributos del cliente[cite: 2, 5].
  4. **`PracticaProgramada1G2.Infraestructure`:** `ClienteRepository.AgregarAsync(cliente)` agrega la entidad al `DbContext` y ejecuta `SaveChangesAsync` persistiendo la información[cite: 5].
  5. **`PracticaProgramada1G2` (Presentación):** Retorna `201 Created` con la ruta de consulta y el recurso creado[cite: 5].

---

### RF-04: Modificar un Cliente
* **Descripción:** Actualiza los datos generales de un cliente existente[cite: 3].
* **Flujo entre Capas:**
  1. **`PracticaProgramada1G2` (Presentación):** `ClientesController` recibe `PUT /api/clientes/{id}` con `ActualizarClienteDto`[cite: 5].
  2. **`PracticaProgramada1G2.Application`:** `ClienteService.ActualizarAsync(id, dto)` busca la existencia del cliente mediante `IClienteRepository`[cite: 5].
  3. **`PracticaProgramada1G2.Infraestructure`:** `ClienteRepository` obtiene la entidad existente desde la base de datos[cite: 5].
  4. **`PracticaProgramada1G2.Domain` / `Application`:** Se actualizan los valores del cliente y se ajustan los teléfonos vinculados según los cambios solicitados[cite: 3, 5].
  5. **`PracticaProgramada1G2.Infraestructure`:** `ClienteRepository` confirma las modificaciones en la base de datos con `SaveChangesAsync`[cite: 5].
  6. **`PracticaProgramada1G2` (Presentación):** Retorna `200 OK` o `204 No Content`[cite: 5].

---

### RF-05: Borrar un Cliente
* **Descripción:** Elimina de manera definitiva un cliente y en cascada todos los teléfonos asociados a él[cite: 3].
* **Flujo entre Capas:**
  1. **`PracticaProgramada1G2` (Presentación):** `ClientesController` recibe `DELETE /api/clientes/{id}`[cite: 5].
  2. **`PracticaProgramada1G2.Application`:** `ClienteService.EliminarAsync(id)` verifica existencia previa[cite: 5].
  3. **`PracticaProgramada1G2.Infraestructure`:** `ClienteRepository` elimina la entidad `Cliente`[cite: 5]. La configuración de la base de datos o de EF Core (`Cascade Delete`) remueve automáticamente los números de teléfono vinculados[cite: 3, 5]. Se ejecuta `SaveChangesAsync`[cite: 5].
  4. **`PracticaProgramada1G2` (Presentación):** Retorna `204 No Content` confirmando la eliminación[cite: 5].