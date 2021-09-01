# PruebaMasiv
Elaboración prueba tecnica de Masiv

Proyecto que tiene las siguientes capas:
 - CommonInterfaces (Interfaces, entidades y enumeraciones en comun y usadas en todos los proyectos).
 - Data (Capa de acceso a datos, se utilizo ADO.NET) la cadena de conexion esta configurada como una variable de entorno.
 - Businness (capa que contine la logica de la aplicación), hace uno de algunas variables de entorno
 - Servicios (Servicios WebApi)
     * utiliza aws.Logger para mensajes de excepción
     * Consta 3 controladores (User, Roulette y Bet).
         1. User --> Tiene los siguientes servicios:
                - Post (Creación de un usuario)
         2. Roulette --> Tiene los siguientes servicios:
                - GetTest (Prueba de arranque)
                - Post (Creación de una ruleta)
                - Put (Abrir una ruleta, actualizarla a abierta)
                - ClosingRoulette (Cerrar una ruleta) retorna resultado de todas las apuestas
                - Get (Listado de ruletas con su respectivo)
         3. Bet --> Tiene los siguientes servicios:
                - Post (Creación de una apuesta asociada a una ruleta)
      * En el servicio Roulette.Get se hace uso de Redis para cache de la consulta.
