# 🚗 StartRider - API de Gerenciamento de Locação de Veículos

StartRider é uma API RESTful desenvolvida com **ASP.NET Core Web API**, voltada para o gerenciamento completo de locações de veículos. Ideal para empresas ou sistemas que necessitam de controle sobre clientes, frota e processos de aluguel de forma segura, escalável e documentada.

---

## 📚 Índice
- [🧰 Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [⚙️ Como Executar](#️-como-executar)
- [🐳 Executando com Docker](#-executando-com-docker)
- [📸 Demonstração](#-demonstracao)

---

## 🧰 Tecnologias Utilizadas
- [.NET 8](https://dotnet.microsoft.com/) - Framework principal
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM para acesso a dados
- [MySql Server](https://dev.mysql.com/doc/) - Banco de dados relacional
- [Swagger / Swashbuckle](https://swagger.io/) - Documentação interativa da API
- [Docker](https://www.docker.com/) - Contêineres para facilitar execução e deploy
- [Serilog](https://serilog.net/) - Logging estruturado

  ---

## ⚙️ Como Executar Localmente

> Requisitos:
> - .NET SDK 8 instalado
> - SQL Server rodando localmente
> - (Opcional) EF Core CLI instalado


```bash
# Clone o repositório
git clone https://github.com/seu-usuario/StartRider.git
cd StartRider

# Restaure pacotes
dotnet restore

# Rode as migrations
dotnet ef database update

# Inicie o servidor
dotnet run

Acesse a documentação Swagger em:
🔗 http://localhost:5000/swagger
🔗 https://localhost:5001/swagger

```
---
## 🐳 Executando com Docker
> Requisitos:
> - Docker instalado
> - Docker compose instalado

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/StartRider.git
cd StartRider

docker-compose up --build

Acesse a documentação Swagger em:
🔗 http://localhost:5000/swagger
🔗 https://localhost:5001/swagger

```
---
## 📸 Demonstração
Ao acessar a api usar o end-point http://localhost:5000/api/usuario e criar um usuário novo para gerar o token e testar a api.
```bash
{
  "email": "string",
  "password": "string",
  "roles": [
    "string"
  ]
}
```
Para a api existem 2 tipos de usuário Admin e Usuario.

> Role Admin tem acesso aos metodos:
> - GET/POST/PUT/DELETE veiculos, Locacao, Locatario

> Role Usuario tem acesso aos metodos:
> - GET/ Veiculos
> - GET/POST/PUT Locacao, Locatario

