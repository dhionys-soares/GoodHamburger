# 🍔 Good Hamburger

Sistema completo para gerenciamento de pedidos, produtos e cardápio de uma hamburgueria, desenvolvido com **.NET**.

---

## 🚀 Tecnologias utilizadas

* .NET (ASP.NET Core)
* Blazor WebAssembly
* Entity Framework Core
* SQL Server
* Docker
* xUnit
* Bootstrap 5

---

## 🏗️ Arquitetura

A solução foi estruturada seguindo os princípios da **Clean Architecture**, com separação clara de responsabilidades entre camadas.

Também foram aplicados conceitos e boas práticas de:

* **DDD (Domain-Driven Design)** → regras de negócio centralizadas no domínio
* **SOLID** → garantindo baixo acoplamento e alta coesão

### Camadas

* **Domain**
  Entidades, regras de negócio, enums e exceções de domínio.

* **Application**
  Services (casos de uso), requests, responses e interfaces.

* **Infrastructure**
  Repositórios, DbContext e acesso ao banco de dados.

* **API**
  Controllers responsáveis por expor os endpoints REST.

* **Web (Blazor)**
  Interface do usuário consumindo a API.

* **Tests**
  Testes automatizados das regras de negócio e serviços.

---

## 📦 Funcionalidades

### 🥪 Produtos

* Criar, listar, atualizar e excluir produtos

### 🧾 Pedidos

* Criar, listar, visualizar, atualizar e excluir pedidos

### 📋 Menu

* Exibir cardápio separado por categorias

---

## ⚙️ Como executar o projeto

### 1. Clonar o repositório

```bash
git clone <https://github.com/dhionys-soares/GoodHamburger>
```

---

### 2. Subir o banco de dados (Docker)

```bash
docker compose up -d
```

---

### 3. Aplicar migrations

```bash
dotnet ef database update
```

---

### 4. Executar a API

```bash
dotnet run --project GoodHamburger.Api
```

---

### 5. Executar o projeto Web

```bash
dotnet run --project GoodHamburger.Web
```

---

## 🌐 Acessos

* Web: `http://localhost:5086`
* API: `http://localhost:5017`

> ⚠️ As portas podem variar dependendo do ambiente e podem ser alteradas manualmente nos arquivos de configuração (`launchSettings.json` ou `Program.cs`).

---

## 🔧 Configuração importante

No projeto **Web**, verifique o `HttpClient`:

```csharp
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5017/")
});
```

---

## 🔗 Endpoints

### Produtos

* `GET /api/products` → retorna todos os produtos cadastrados
* `GET /api/products/{id}` → retorna um produto específico
* `POST /api/products` → cria um novo produto
* `PUT /api/products/{id}` → atualiza um produto existente
* `DELETE /api/products/{id}` → remove um produto

---

### Pedidos

* `GET /api/orders` → retorna todos os pedidos
* `GET /api/orders/{id}` → retorna os detalhes de um pedido
* `POST /api/orders` → cria um novo pedido
* `PUT /api/orders/{id}` → atualiza um pedido existente
* `DELETE /api/orders/{id}` → remove um pedido

---

### Menu

* `GET /api/menu` → retorna o cardápio agrupado por categorias (sanduíches, acompanhamentos e bebidas)

---

## 🧪 Testes

Executar todos os testes:

```bash
dotnet test
```

---

## ⚠️ Observações

* Popular o banco de dados utilizando o sistema em products/create

---

## 👨‍💻 Autor

Dhionys Soares
