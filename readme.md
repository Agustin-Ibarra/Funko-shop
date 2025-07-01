# Funko Shop aplicacion web
Esta aplicacion permite ver el catalogo de preductos, poder buscar por nombre o categoria, y ordenar por precio. Cuenta con un carrito de compras para poder almacenar los articulos, modificando la lista, acorde a la cantidad de articulos, gustos y costos finales. El usuario podra acceder a su cuenta para poder revisar su informacion como historial de compra, etc.
## Tabla de contenido
1. [Características](#características)
3. [BackEnd](#backEnd)
4. [Base de datos](#base-de-datos)
5. [Documentacion](#documentacion)
<!-- 6. [Pruebas unitarias](#pruebas-unitarias)
7. [Monitoreo de rutas](#monitoreo-de-rutas) -->
2. [FrontEnd](#frontend)
## Características
- Gestión de usuarios implementacion de tokens de autorizacion y autenticacion
<!-- - Integración con API de terceros, utiliza los servicios de stripe para realizar pagos online -->
- Implementacion de varibles de entorno para el acceso a diferentes servicios y otros usos especificos
- Optimizacion de APIs mediante utlizacion de cache y limitando la cantidad de peticiones
## BackEnd
- Tecnologias utilizadas: C# .NET Core ASP.NET dotnet 
```
App/
│
├── Controllers/   # Procesan las peticiones y logica de las respuestas
├── Data/          # Configuracion del constructor de AppDbContext y entidades
├── DTOs/          # Definicion de las clases de modelos de datos para transacciones
├── Logs/          # Configuracion de logs y monitoreo de la aplicacion
├── Models/        # Definicion de clases que representan las entidades de la base de datos
├── Repository/    # Archivos que contienen las clases que interactuan con la base de datos
└── Program/       # punto de entrada de la aplicacion (configuracion del servidor)
```
## Base de datos
La informacion esta almacenada en una base de datos relacional, utiliza la libreria de sequelize para definir los medelos, conexion e interaccion con la base de datos a travez de un ORM, esta diseñada y graficada con la herramienta [dbdiagram.io](https://dbdiagram.io/)
- Base de datos relacional
- Modelos y consultas utilizando ORM Entity framework
- Gestor de base de datos: MySql
## Documentacion
- Documentacion de APIs: la documentacion de los endpoint y APIs fue creada con Swagger Open.io
- Documentacion del codigo fuente: la documentacion del codigo fuente fue creada con JSDoc
- Enlace: documentacion disponible en [docs](http://localhost:3001/cinemark/documentation)
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
- Monitoreo: se realiza mediante un middleware y la definnicion de la clase que contiene la logica para crear los logs personalizados
## FrontEnd
- Tecnologias utilizadas: Razor CSS3 JavaScript
```
App/
|──Views/
|   └── views/ # archivos de vistas
|──wwwroot/
|   ├── css/   # archivos de estilos
|   └── js/    # archivos javascripts
```
## Inicio
- Inicio de la aplicacion: una vez clonado el repositorio se debe escribir el siguiente comando en la terminal
```bash
dotnet run
```
<!-- ## Instalacion
- Dependencias: para instalar las dependencias necesarias para el correcto funcionamineto de la applicacion, ejecuta el siguiente comando en la terminal
```bash
npm install
``` -->
