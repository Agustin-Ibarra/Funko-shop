# Funko Shop - Aplicación Web
Este proyecto fue desarrollado con el objetivo de aprender y reforzar conocimientos en áreas como patrones de diseño, arquitecturas de software y buenas prácticas de desarrollo, aplicándolos en el proyecto según las tecnologías utilizadas.

Funko Shop es una aplicación web que permite a los usuarios explorar un catálogo de productos, buscar artículos por nombre o categoría, y ordenarlos por precio. La plataforma incluye un carrito de compras donde se pueden agregar, modificar o eliminar productos, ajustando la lista según la cantidad, las preferencias del usuario y el costo final. Además, los usuarios pueden iniciar sesión para realizar compras, agregar articulos a su carrito, acceder a su cuenta, revisar su información personal y consultar su historial de compras.
## Tabla de contenido
1. [Características](#características)
3. [BackEnd](#backEnd)
4. [Base de datos](#base-de-datos)
5. [Documentacion](#documentacion)
<!-- 6. [Pruebas unitarias](#pruebas-unitarias)
7. [Monitoreo de rutas](#monitoreo-de-rutas) -->
2. [FrontEnd](#frontend)
## Características
- Gestión de usuarios implementación de tokens de y autenticación mediante cookies
<!-- - Integración con API de terceros, utiliza los servicios de stripe para realizar pagos online -->
- Implementación de variables de entorno para el acceso a diferentes servicios y otros usos específicos
- Optimización de APIs mediante la implementacion de cache, optimizacion de las consultas mediante indices, y la limitacion del numero de peticiones
## BackEnd
- Tecnologías utilizadas: C# .NET Core ASP.NET dotnet 
```
App/
│
├── Controllers/   # Procesan las peticiones y contienen la lógica de las respuestas
├── Data/          # Configuración del constructor de AppDbContext y definición de entidades
├── DTOs/          # Clases que representan modelos de datos utilizados en transacciones
├── Logs/          # Configuración de logs y monitoreo de la aplicación
├── Models/        # Clases que representan las entidades de la base de datos
├── Repository/    # Clases que interactúan con la base de datos mediante Entity Framework
└── Program/       # Punto de entrada de la aplicación (configuración del servidor)
```
## Base de datos
La percistencia de informacion se realiza a traves de una base de datos relacional, la misma esta diseñada y graficada con la herramienta [dbdiagram.io](https://dbdiagram.io/), el BackEnd utiliza Entity Framework para la interacción con la base de datos, a través de un ORM
- Base de datos relacional
- Modelos y consultas utilizando un ORM de Entity Framework, implementa paginación y DTOs para optimizar las consultas y no exponer las entidades
- Gestor de base de datos: MySql
## Documentacion
- Documentación de APIs: la documentación de los endpoint y APIs esta creada con Swagger Open.io
- Enlace: documentación disponible en [docs](http://localhost:3001/documentation)
<!-- ## Pruebas unitarias
- Librerias: las pruebas unitarias estan creadas con la libreria de Jest y Mock
- Iniciar test: con el siguiente comando ejecuta las pruebas unitarias
```bash
npm test
```
## Pruebas de integracion
- Librerias: las pruebas de integracion estan creadas con las librerias de Jest y supertest
- Iniciar test: con el siguiente comando ejecuta las pruebas unitarias
```bash
npm test
``` -->
## Monitoreo de rutas
- Monitoreo: se realiza mediante un middleware y la implementacion de la clase que contiene la lógica para crear los logs personalizados
## FrontEnd
- Tecnologías utilizadas: Razor CSS3 JavaScript
```
App/
|── Views/
|   └── views/ # archivos de vistas Razor
|── wwwroot/
|   ├── css/   # archivos de estilos
|   └── js/    # archivos javascripts
```
## Inicio
- Inicio de la aplicación: una vez clonado el repositorio se debe escribir el siguiente comando en la terminal
```bash
dotnet run
```
<!-- ## Instalacion
- Dependencias: para instalar las dependencias necesarias para el correcto funcionamineto de la applicacion, ejecuta el siguiente comando en la terminal
```bash
npm install
``` -->
