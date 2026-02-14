# 🧠 API de Gestão de Consultas (Psicologia)
Trabalho de Conclusão de Curso Curso Desenvolvimento de Sistemas pela Etec Comendador João Rays

"API para gerenciar o fluxo de atendimento entre psicólogos e pacientes"

## 🛠️ Tecnologias e Frameworks
1. Runtime: .NET 9.0
2. ORM: Entity Framework Core (Suporte a SQL Server e MySQL via Pomelo)
3. Segurança: ASP.NET Core Identity & JWT (JSON Web Tokens)
4. Documentação: Swagger (OpenAPI)

## 🚀 Como executar o projeto
### 1. Clone o repositório:
```
git clone https://github.com/najumattos/Connectamente/
```
### 2. Configuração do Banco de Dados:
No arquivo appsettings.json, ajuste a sua ConnectionString. O projeto está preparado para rodar tanto em SQL Server quanto em MySQL.

### 3. Atualize o Banco (Migrations):
```
dotnet ef database update
```

### 4. Rode a aplicação:
```
dotnet run
```
### 5. Acesse o Swagger:
```
https://localhost:5256/
```

## 🔐 Autenticação
A API utiliza JWT. Para acessar os endpoints protegidos:

1. Faça login no endpoint /login.
2. Copie o token gerado.
3. No Swagger, clique no botão Authorize e cole o token.
