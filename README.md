# Teste CQRS
## Descrição do Projeto
<p align="center">🚀 O projeto tem como objetivo mostrar um exemplo de uso do cqrs com uma base de escrita no sql server e uma de leitura no mongoDB</p>
<p align="center">
 <a href="#objetivo">Objetivo</a> •
 <a href="#instalacao">Como usar</a> • 
 <a href="#tecnologias">Tecnologias</a> • 
</p>

<h4 align="center"> 
	🚧  Status do projeto 🚀 Em construção...  🚧
</h4>

### 🛠 Objetivo

Demostrar o uso de cqrs,  o projeto é crud de pessoa básico onde ao salvar uma pessoa lançará um envento para salvar em um banco nosql
ele esta separado em [QueryStack](https://github.com/Lucas-Sampaio/ExemploCQRS/tree/master/src/API/Application/Queries) onde tem uma classe que vai ser responsavel por trazer as
consultas do banco mongodb
e uma [CommandStack](https://github.com/Lucas-Sampaio/ExemploCQRS/tree/master/src/API/Application/Commands) que vai ser responsavel por fazer a escrita no banco sql server

### 🛠 Como usar
 1. Baixe o projeto
 2. rode o comando no powershell na pasta [docker](https://github.com/Lucas-Sampaio/ExemploCQRS/tree/master/Docker) -> docker-compose -f cqrs-compose.yml up -d
 isso irá subir os serviços necessario pro projeto.
 3. Com o projeto aberto atualize a base rode -> update-database -Context ProjetoContext 
  isso irá criar a base no sql server

### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [QueryStack](https://github.com/Lucas-Sampaio/ExemploCQRS/tree/master/src/API/Application/Queries)
- [.Net 5](https://github.com/dotnet)
- [Ef Core Sql Server](https://github.com/dotnet/efcore)
- [MongoDBDriver](https://github.com/mongodb/mongo-csharp-driver)
- [Kibana](https://www.elastic.co/guide/en/kibana/current/index.html) - usado para ver os logs do elastic
- [Elastic Search](https://www.elastic.co/pt/)
- [Xabaril/HealthCheck](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)
