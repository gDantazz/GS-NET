# GS-NET - Projeto DevOps & Cloud Computing

## Descrição do Projeto

Este projeto consiste na implementação de um ambiente de desenvolvimento em nuvem, utilizando uma **VM Linux** para o Back-End com **API .NET** e banco de dados **SQLite**, simulando a separação entre Front-End e Back-End.

O objetivo do projeto é demonstrar:

- Configuração de ambiente em nuvem
- Persistência de dados utilizando banco SQLite
- Realização de operações CRUD (Create, Read, Update, Delete)
- Documentação de evidências do funcionamento da aplicação

---

## Estrutura do Projeto

GS-NET/
├── Controllers/ # Controladores da API
├── Models/ # Modelos de dados
├── Properties/
├── appsettings.json # Configurações da API
├── GS-NET.csproj # Arquivo do projeto .NET
└── Program.cs # Ponto de entrada da aplicação

yaml
Copiar código

---

## Requisitos

- VM Linux (AlmaLinux 10)
- SQLite (`worktime_full.db`) para persistência
- .NET Runtime (7.0.x) portátil ou instalado localmente

---

## Configuração e Execução

1. **Clonar o repositório**

bash
git clone https://github.com/gDantazz/GS-NET.git
cd GS-NET
Instalar dependências e restaurar pacotes

bash
Copiar código
dotnet restore
Build e execução da API

bash
Copiar código
dotnet build
dotnet run
Obs.: Caso o .NET SDK não esteja instalado globalmente, é possível usar a versão portátil baixada como tar.gz na VM e definir as variáveis DOTNET_ROOT e PATH conforme documentação do projeto.

Banco de Dados e CRUD
O banco SQLite worktime_full.db é utilizado para persistência de dados.

Exemplos de operações CRUD:

sql
Copiar código
-- Create
INSERT INTO usuarios (nome, email) VALUES ('João Silva', 'joao@email.com');

-- Read
SELECT * FROM usuarios;

-- Update
UPDATE usuarios SET email = 'novoemail@email.com' WHERE nome = 'João Silva';

-- Delete
DELETE FROM usuarios WHERE nome = 'João Silva';
Evidências
Prints do terminal mostrando diretório do projeto e banco SQLite

Prints de comandos CRUD executados

Vídeo demonstrativo: [link do YouTube]

Mostrar passo a passo desde o acesso à VM até a persistência dos dados

Observações
Devido a limitações técnicas e de tempo, não foi possível criar a VM Windows, mas a solução apresentada cumpre os objetivos do projeto.

A persistência de dados foi totalmente validada dentro da VM Linux.

O projeto está pronto para execução local ou em ambiente de nuvem usando a VM Linux fornecida.
