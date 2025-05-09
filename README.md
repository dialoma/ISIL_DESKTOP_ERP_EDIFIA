# 📌 ISIL_DESKTOP_ERP_EDIFIA

Edifia is a desktop ERP system developed as an academic project at the Instituto San Ignacio de Loyola (ISIL). Its main objective is to streamline condominium management, covering areas such as resident administration, payments, incident tracking, maintenance, and more.

This software was designed to provide a comprehensive, user-friendly, and adaptable solution for managing residential buildings.

## 🧩 Main Features

Resident and apartment management

Visitor registration and access control

Maintenance personnel administration

Report generation

## 🛠️ Technologies Used
Language: C#

Framework: .NET Framework (WinForms)

Database: Microsoft SQL Server

IDE: Visual Studio 2022

🚀 How to Run the Project
1. Clone the repository
   ```bash
   git clone https://github.com/dialoma/ISIL_DESKTOP_ERP_EDIFIA.git
2. Import the database
Go to the BDD folder in the repository.

Use SQL Server Management Studio (SSMS) to run the SQL scripts to create and populate the database.

Ensure the database is correctly configured.
3. Open the solution
Open the Edifia.sln file in Visual Studio.
4. Set up the connection string
Update the connection string in App.config to match your local SQL Server setup:
```xml
<connectionStrings>
  <add name="ConexionSQL"
       connectionString="Data Source=YOUR_SERVER;Initial Catalog=edifia_db;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```
5. Run the application
Build the solution and run it from Visual Studio.

## 👥 Development Team
This project was developed by students from the Instituto San Ignacio de Loyola (ISIL):

Eduardo Kohatsu Asato

Diego Arturo López Maya

Davier Alejandro López Vargas

Daniel Esteban Moreno Rodriguez

## 📄 License
This project was developed for educational purposes. Use in production environments requires further validation and customization.

# 🇪🇸 Versión en Español
# 📌 ISIL_DESKTOP_ERP_EDIFIA
Edifia es un sistema ERP de escritorio desarrollado como proyecto académico en el Instituto San Ignacio de Loyola (ISIL). Su objetivo es optimizar la gestión de condominios, permitiendo controlar áreas como la administración de residentes, pagos, incidencias, mantenimiento, y más.

Este software fue diseñado para brindar una solución integral, fácil de usar y adaptable a las necesidades de la administración de edificios residenciales.

## 🧩 Características principales

Gestión de residentes y departamentos

Registro de visitas y control de accesos

Administración de personal de mantenimiento

Generación de reportes

## 🛠️ Tecnologías utilizadas
Lenguaje: C#

Framework: .NET Framework (WinForms)

Base de datos: Microsoft SQL Server

IDE: Visual Studio 2022

🚀 How to Run the Project
1. Clona este repositorio
   ```bash
   git clone https://github.com/dialoma/ISIL_DESKTOP_ERP_EDIFIA.git
2.Importa la base de datos
Dirígete a la carpeta BDD del repositorio.

Usa SQL Server Management Studio (SSMS) para ejecutar los scripts de creación e inserción de datos.

Asegúrate de que la base de datos quede correctamente configurada.

3. Abre la solución
Abre el archivo Edifia.sln en Visual Studio.
4. Configura la cadena de conexión
Modifica la cadena de conexión en el archivo App.config para que apunte a tu servidor SQL local:
```xml
<connectionStrings>
  <add name="ConexionSQL"
       connectionString="Data Source=YOUR_SERVER;Initial Catalog=edifia_db;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```
5. Ejecuta la aplicación
Compila y ejecuta el proyecto desde Visual Studio.

## 👥 Equipo de desarrollo
Este proyecto fue desarrollado por estudiantes del Instituto San Ignacio de Loyola (ISIL):

Kohatsu Asato, Eduardo

López Maya, Diego Arturo

López Vargas, Davier Alejandro

Moreno Rodriguez, Daniel Esteban

## 📄 Licencia
Este proyecto fue desarrollado con fines educativos. Su uso en entornos productivos requiere ajustes y validaciones adicionales.
