## How to use:
- You will need Visual Studio 2017 (preview 15.3) and the .NET Core SDK (released in August 14, 2017).
- The latest SDK and tools can be downloaded from https://dot.net/core. 
- Read the .NET Core 2.0 [release announcement](https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-net-core-2-0/) for more information.

Also you can run the Equinox Project in Visual Studio Code (Windows, Linux or MacOS).

To know more about how to setup your enviroment visit the [Microsoft .NET Download Guide](https://www.microsoft.com/net/download)

## How to setup via docker compose:

```sh
$ cd /deploy/docker-compose
$ docker-compose pull
$ docker-compose build
$ docker-compose up
```
For stop all containers
```sh
$ docker-compose down
```


And open links below as ;
- Api -> localhost:8003/swagger
- connect to DB -> localhost:1433 | user:sa | password:testuser123!
- logging -> localhost:5601 (you need set index as logstash-* by @timestamp AND product-* by @timestamp (after any perform request and It will create index as product automatically))

## How to setup via docker swarm:

```sh
$ cd /deploy/docker-swarm
$ sh setup.sh
$ docker service scale {service_name}={replica_num}
```
And open links below as ;
- Api -> 192.168.99.100,101,102:8003/swagger
- connect to DB -> 192.168.99.100,101,102:1433 | user:sa | password:testuser123!
- visualizer -> 192.168.99.100,101,102:8080
- portainer (container dashboad UI) -> 192.168.99.100,101,102:9000
    - choose an user and password when entrance to portainer registeration page
- logging -> 192.168.99.100,101,102:5601 (you need set index as logstash-* by @timestamp AND product-* by @timestamp (after any perform request and It will create index as product automatically))

- PLEASE SHOULD BE RUNNING STATE OF DOCKER-MACHINES
- if you got error about machine driver as "Unable to query docker version ..." then run command as below ;

```sh
docker-machine regenerate-certs swarm-1
docker-machine regenerate-certs swarm-2
docker-machine regenerate-certs swarm-3
```

Note:

## Technologies implemented:

- ASP.NET Core 2.0 (with .NET Core)
 - ASP.NET MVC Core 
 - ASP.NET WebApi Core
 - ASP.NET Identity Core
- Entity Framework Core 2.0
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI

## Architecture:

- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- CQRS (Imediate Consistency)
- Event Sourcing
- Unit of Work
- Repository and Generic Repository

## Deployment:
- Docker Compose
- Docker Swarm Mode on VMBox (as virtualbox etc...)

## About:
The Equinox Project was developed by [Bilal İslam] under the [MIT license](LICENSE).
