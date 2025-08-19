# PacientesAPI

Proyecto para la gestion de Pacientes y Citas.

Proyecto principal PacientesAPI

Proyecto para Gestion de la Base de datos Pacientes.DataBase

##Generalidades

El proyecto utiliza contenedores docker linux, por lo cual es necesario contar con docker instalado.

El proyecto principal PacientesAPI debe ser ejecutado para iniciarlo, al iniciarse, se corren automaticamente los migraciones las cuales
usan una base de datos SQL Lite.

Cada endpoint utiliza autenticacion JWT, al correr las migracion se autogenera un usuario admin, con password Admin123*

Con los endpoint de pacientes se pueden realizar las siguientes funciones:
listar, crear, actualizar, eliminar pacientes.

Trama para crear un paciente:

```
{
  "cedula": "string",
  "nombreCompleto": "string",
  "edad": 0
}
```

Nota: aunque en Swagger sugiera crear una cita, no es obligatorio hacerlo.

Con los endpoint de citas se pueden realizar las siguientes funciones:
listar, crear, actualizar, cancelar citas.

Para crear una cita se necesita agendar una fecha y hora, esta debe ir en formato "dd/MM/yyyy hh:mm tt"

Ejemplo:

```
{
  "tipo": "General",
  "fecha": "25/08/2025 10:30 am",
  "estado": "pendiente"
}
```