🌱 WorkTimePanel.Full — Sistema de Gestão de Usuários & Autenticação

Projeto desenvolvido para Advanced Business Development with .NET – FIAP (2º Semestre / 2TDS)

API REST construída com ASP.NET Core 8, Entity Framework Core, JWT Authentication e arquitetura em camadas, permitindo gerenciamento de usuários, login seguro e integração futura com outros serviços como WorkTime Panel em Java.

🎯 Objetivo Geral

O WorkTimePanel.Full é uma API REST responsável por autenticação, registro e administração de usuários do sistema WorkTime.
O backend oferece:

Cadastro e login com JWT

Controle de perfis (Role)

Hash seguro de senhas (SHA-256)

Seed automático de usuário Admin (RH)

Persistência com SQLite + Migrations

Arquitetura limpa e modular para integração com front-end ou outros microserviços

Nesta entrega, o foco foi criar toda a estrutura da API, incluindo migrations, seed, controllers, camada de domínio, serviços, injeção de dependência, documentação Swagger e execução via CLI e Visual Studio.

🏗 Arquitetura da Solução

O projeto segue rigorosamente uma arquitetura em camadas, semelhante ao padrão Clean/DDD:

WorkTimePanel_Full/
/Application        → Casos de uso, DTOs, serviços de aplicação
/Domain             → Entidades, agregados, invariantes e interfaces de domínio
/Infrastructure     → DbContext, repositórios concretos, configurações de dados
/Migrations         → Migrations do Entity Framework Core
/Web                → Controllers, endpoints, configuração da API
/bin                → Build gerado pelo .NET
/obj                → Arquivos temporários de build
Program.cs          → Configuração principal da aplicação (Minimal API)
/appsettings.json   → Configurações, connection string, etc.
/worktime_full.db   → Banco de dados SQLite gerado automaticamente
WorkTimePanel.Full.csproj  → Arquivo de projeto .NET
WorkTimePanel.sln           → Solução utilizada no Visual Studio

✔ Tecnologias Utilizadas

.NET 8

Entity Framework Core 8

SQLite

Swagger / OpenAPI

JWT Bearer Authentication

C# 12

Injeção de dependências (DI)

⚙️ Banco de Dados e Configuração

O sistema utiliza SQLite, ideal para testes locais e fácil deploy.

Connection String (appsettings.json)
"ConnectionStrings": {
  "DefaultConnection": "Data Source=worktime_full.db"
}

🧬 Migrations + Seed
Aplicar migrations:
dotnet ef database update


ou com ferramenta local:

dotnet tool restore
dotnet dotnet-ef database update

Seed de Admin (executado automaticamente)

Ao iniciar a API, se o banco não tiver usuários, o seed cria:

Usuário: admin
Senha:   Admin@123
Role:    RH


Senhas são armazenadas com SHA-256.

🔐 Autenticação (JWT)

O login retorna um JWT válido por 120 minutos, configurado em:

"Jwt": {
  "Key": "ThisIsADevSecretKeyReplaceInProduction_ChangeMe",
  "Issuer": "WorkTimePanel",
  "Audience": "WorkTimePanelUsers",
  "ExpireMinutes": 120
}

🚀 Como Executar o Projeto
1️⃣ Pré-requisitos

.NET SDK 8+

Visual Studio 2022 ou

VS Code / Rider

2️⃣ Clonar o Repositório
git clone <url-do-repo>
cd WorkTimePanel_Full

3️⃣ Restaurar Dependências
dotnet restore

4️⃣ Criar o Banco e Aplicar Migrations
dotnet tool restore
dotnet dotnet-ef database update

5️⃣ Executar a API
Via CLI
dotnet run --project WorkTimePanel.Full.csproj

Via Visual Studio

clique duas vezes no arquivo da solução

Selecione o perfil:

WorkTimePanel.Full



E pressione ▶ Run.

🌐 Swagger (Documentação)

Ao rodar a API:

https://localhost:62902/swagger


ou

http://localhost:62903/swagger

🧩 Endpoints Principais
🔐 Autenticação
Método	Rota	Descrição
POST	/auth/login	Gera JWT e autentica usuário
👤 Usuários
Método	Rota	Descrição
GET	/users	Lista usuários
GET	/users/{id}	Obtém usuário
POST	/users	Cria usuário
PUT	/users/{id}	Atualiza usuário
DELETE	/users/{id}	Remove usuário
📦 Exemplo de Login (JSON)
POST /auth/login


Body:

{
  "username": "admin",
  "password": "Admin@123"
}


Resposta:

{
  "token": "...jwt..."
}

🧠 Requisitos Atendidos (Checklist)
Requisito	Status
Domínio & Arquitetura (20 pts)	✅
Regras de negócio & invariantes	✅
Camada Application (20 pts)	✅
DTOs + Services + validação	✅
Tratamento de erro / ProblemDetails	⚠ (básico implementado)
Infra & Dados (20 pts)	✅
EF Core + Migrations + Seed	✅
Web API (30 pts)	⚠ (CRUD completo, sem /search ainda)
Documentação (README) (10 pts)	✅

Pontuação estimada: 85–95 pts, dependendo dos critérios do professor.

👨‍💻 Integrantes
Nome	RM
Gustavo Dantas	RM560685
Paulo Neto	RM560262
Davi Vasconcelos Souza	RM559906
🏁 Conclusão

A API WorkTimePanel.Full fornece toda a base necessária para autenticação, gestão de usuários e integração com sistemas externos.
A arquitetura modular facilita o crescimento do projeto e segue as boas práticas sugeridas para o Challenge e para a disciplina.