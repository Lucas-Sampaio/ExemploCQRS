# Teste CQRS
## Descrição do Projeto
<p align="center">🚀 O projeto tem como objetivo mostrar um exemplo de uso do cqrs com uma base de escrita no sql server e uma de leitura no mongoDB</p>

<h4 align="center"> 
	🚧  Status do projeto 🚀 Em construção...  🚧
</h4>

### 🛠 Objetivo

Demostrar o uso de cqrs,  o projeto é crud de pessoa básico onde ao salvar uma pessoa lançará um envento para salvar em um banco nosql
ele esta separado em [QueryStack](https://github.com/Lucas-Sampaio/ExemploCQRS/tree/master/src/API/Application/Queries) onde tem uma classe que vai ser responsavel por trazer as
consultas do banco mongodb
e uma [CommandStack](https://github.com/Lucas-Sampaio/ExemploCQRS/tree/master/src/API/Application/Commands) que vai ser responsavel por fazer a escrita no banco sql server
#### Fluxo do projeto
![Alt text](/Assets/FluxoCqrs2.png?raw=true "Fluxo")

### 🛠 Como usar
 1. Baixe o projeto
 2. rode o comando no powershell na pasta [docker](https://github.com/Lucas-Sampaio/ExemploCQRS/tree/master/Docker) -> ```docker-compose -f cqrs-compose.yml up -d```
 isso irá subir os serviços necessario pro projeto.
 3. Com o projeto aberto atualize a base rode ->```update-database -Context ProjetoContext``` 
  isso irá criar a base no sql server
 4. O projeto gera a documentação da api automatica pelo swagger mas pode baixar a [collection](https://github.com/Lucas-Sampaio/ExemploCQRS/blob/master/Assets/TesteCqrs.postman_collection) do postman se preferir

### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [.Net 5](https://github.com/dotnet)
- [Ef Core Sql Server](https://github.com/dotnet/efcore)
- [MongoDBDriver](https://github.com/mongodb/mongo-csharp-driver)
- [Kibana](https://www.elastic.co/guide/en/kibana/current/index.html) - usado para ver os logs do elastic
- [Elastic Search](https://www.elastic.co/pt/)
- [Xabaril/HealthCheck](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)
 #### HealthCheck Uri - localhost:'porta'/api/hc-ui
  ![Alt text](/Assets/healthcheck.png?raw=true "Fluxo")
