# Minimal API - API de Veículos

Este projeto é uma API de veículos construída com **ASP.NET Minimal API** e .NET 9. Ele inclui autenticação JWT, endpoints para administração de veículos e administradores, além de testes unitários e funcionais.

---

📂 Estrutura do Projeto

minimal-api/
├─ Api/ # Projeto principal da API  
│ ├─ Startup.cs # Configuração da API  
│ ├─ Program.cs # Entry point  
│ ├─ Dominio/ # Entidades, DTOs, Serviços, ModelViews e Enums  
│ └─ Infraestrutura/Db/ # Contexto do banco de dados  
├─ Test/ # Projeto de testes  
│ ├─ Helpers/Setup.cs # Setup de testes com WebApplicationFactory  
│ ├─ Mocks/ # Mocks de serviços  
│ ├─ Requests/ # Testes de integração (Requisições)  
│ └─ Domain/Servicos/ # Testes unitários de serviços  
├─ minimal-api.sln # Solution  

---

🛠 Tecnologias Utilizadas

- .NET 9  
- ASP.NET Minimal API  
- Entity Framework Core (MySQL)  
- JWT para autenticação  
- Swagger/OpenAPI para documentação  
- MSTest para testes unitários e de integração  

---

🔑 Funcionalidades

### Administradores
- Login com JWT  
- CRUD de administradores (somente para perfis autorizados)  

### Veículos
- CRUD completo de veículos  
- Validação de dados na criação e atualização  
- Paginação opcional nos endpoints de listagem  

### Testes
- Testes unitários dos serviços  
- Testes de integração das APIs usando `WebApplicationFactory`  

---

## ⚙️ Configuração

1. Clone o repositório:
```bash
git clone <URL_DO_REPOSITORIO>
Acesse a pasta do projeto:

bash
Copiar código
cd minimal-api/Api
Configure a string de conexão com o MySQL no appsettings.json:

json
Copiar código
{
  "ConnectionStrings": {
    "MySql": "server=localhost;database=minimal_api;user=root;password=1234"
  },
  "Jwt": "SUA_CHAVE_SECRETA_AQUI"
}
Rode a aplicação:

bash
Copiar código
dotnet run
Acesse a documentação Swagger:

bash
Copiar código
http://localhost:5000/swagger
⚠️ Observações sobre testes
Atualmente, a parte de testes unitários e funcionais ainda apresenta alguns erros, principalmente relacionados à inicialização de mocks e ao contexto da aplicação durante os testes funcionais.

Haverá melhorias futuras para garantir que todos os testes passem corretamente, incluindo ajustes nos mocks, configuração do ambiente de teste e validação de dados iniciais.

📝 Autor
Alesson Carlos