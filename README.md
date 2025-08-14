# WinForms CRUD Clientes

Este proyecto es una aplicación desarrollada en Windows Forms utilizando Entity Framework 6 para conectarse a una base de datos SQL Server alojada de forma remota. La aplicación permite realizar operaciones CRUD (crear, leer, actualizar y eliminar) sobre una tabla de clientes que contiene los campos: Id, cédula, nombre, teléfono, correo y dirección.

## Descripción del desarrollo

El sistema fue creado como parte de una evaluación práctica. La interfaz principal carga los clientes en un `DataGridView` y permite gestionarlos mediante botones para cada operación. La conexión a la base de datos se realiza a través de una cadena de conexión definida en el archivo `App.config`, utilizando un servidor SQL remoto proporcionado para la práctica.

Durante el desarrollo, se presentó un problema relacionado con archivos temporales y de configuración interna de Visual Studio almacenados en la carpeta `.vs`. Estos archivos generaban errores de permisos y afectaban el correcto manejo del proyecto. Para evitar inconvenientes al trabajar en otros entornos, se recomienda excluir dichas carpetas y archivos que no son necesarios para la ejecución del programa.

## Consideraciones al ejecutar el proyecto

1. Contar con **Visual Studio 2022** o superior, con el paquete de desarrollo para .NET Framework 4.7.2 instalado.
2. Verificar que la cadena de conexión en `App.config` apunte al servidor y base de datos correctos.
3. Entity Framework 6 debe estar instalado como dependencia del proyecto.
4. No modificar ni eliminar el `DbContext` ni las configuraciones de conexión, ya que están preparados para la base de datos remota.
5. Restaurar los paquetes NuGet antes de ejecutar el proyecto por primera vez.

## Autor

Este proyecto fue desarrollado por el **Ingeniero en Tecnologías de la Información William Cubero Navarro**, como parte de un ejercicio práctico para demostrar conocimientos en desarrollo de aplicaciones de escritorio con .NET Framework, gestión de bases de datos remotas y uso de Entity Framework en entornos reales.
