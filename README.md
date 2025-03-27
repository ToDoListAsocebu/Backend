# 📝 ToDoApp

ToDoApp es una API REST construida en **.NET 8** que permite gestionar tareas (To-Do List). La aplicación está dockerizada y desplegada en Azure con un **pipeline CI/CD automatizado**.


🚀 **Desarrollado por Camilo Chaparro - Desarrollador de Software**

---


## 🔧 Tecnologías
- **.NET 8**
- **Entity Framework Core**
- **SQL Server (Azure SQL Database)**
- **Docker**
- **GitHub Actions (CI/CD)**
- **Azure Web Apps**

## 🛠️ Configuración Local

### 📌 1. Clonar el repositorio
```bash
git clone https://github.com/caachaparrosi/ToDoListAsocebu/Backend.git
cd Backend
```

### 📌 2. Configurar la base de datos
Configura la cadena de conexión en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:tu-servidor.database.windows.net;Database=ToDoAppDb;User Id=tu-usuario;Password=tu-password"
}
```

### 📌 3. Ejecutar la aplicación
```bash
dotnet run --project ToDoApp.API
```

La API estará disponible en `http://localhost:5000`

## 🐳 Ejecución con Docker

### 📌 1. Construir y ejecutar el contenedor
```bash
docker build -t todoapp-api .
docker run -p 8080:8080 todoapp-api
```

La API estará disponible en `http://localhost:8080`

## 🚀 Despliegue en Azure con CI/CD
Cada vez que se hace un **push a `main`**, GitHub Actions:
1. **Compila y prueba** el código.
2. **Construye la imagen Docker**.
3. **Sube la imagen a Docker Hub**.
4. **Despliega automáticamente en Azure Web Apps**.

## 📤 CI/CD con GitHub Actions
Ubicación del pipeline: `.github/workflows/cicd.yml`

## 📬 Endpoints Principales

### 📌 Obtener todas las tareas
```http
GET /api/Task
```
### 📌 Obtener tarea
```http
GET /api/Task/{id}
```

### 📌 Crear una nueva tarea
```http
POST /api/Task
```

### 📌 Actualizar una tarea
```http
PUT /api/Task/{id}
```

### 📌 Eliminar una tarea
```http
DELETE /api/Task/{id}
```

