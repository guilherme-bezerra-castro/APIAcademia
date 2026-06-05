# APIAcademia

> Web API REST para gerenciamento de alunos e planos de uma academia, desenvolvida com ASP.NET Core e Entity Framework Core.

O APIAcademia é um projeto de estudo e portfólio que simula o back-end de um sistema de gerenciamento de academia. A API permite cadastrar e gerenciar alunos e planos de assinatura, com foco em boas práticas de desenvolvimento como separação de responsabilidades, tratamento global de erros e validação de dados.

## Tecnologias
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger

## Arquitetura e padrões

- **DTOs** — separação entre as entidades do banco e os dados expostos pela API, implementados como `record` do C#
- **Services** — camada de serviço isolando a lógica de negócio dos controllers
- **Model Binding** — suporte a filtros via query string (`?ativo=true&planoId=1`)
- **Injeção de dependência** — configurada via `Program.cs` com tempo de vida `Scoped`

## Endpoints

### Alunos

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/alunos` | Lista todos os alunos |
| GET | `/alunos/{id}` | Busca aluno por ID |
| GET | `/alunos/primeiro` | Retorna o primeiro aluno cadastrado |
| GET | `/alunos/filtrar?ativo=true&planoId=1` | Filtra alunos por status e plano |
| POST | `/alunos` | Cadastra novo aluno |
| PUT | `/alunos/{id}` | Atualiza dados do aluno |
| PATCH | `/alunos/{id}/status` | Ativa ou desativa um aluno |
| PUT | `/alunos/{id}/plano` | Troca o plano de um aluno |
| DELETE | `/alunos/{id}` | Remove um aluno |

### Planos

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/planos` | Lista todos os planos |
| GET | `/planos/{id}` | Busca plano por ID |
| GET | `/planos/alunos` | Lista planos com seus alunos vinculados |
| GET | `/planos/{id}/alunos` | Lista alunos de um plano específico |
| POST | `/planos` | Cria novo plano |
| PUT | `/planos/{id}` | Atualiza um plano |
| DELETE | `/planos/{id}` | Remove um plano |

## Como executar localmente

### Pré-requisitos

- .NET 8 SDK
- MySQL

### Passos

1. Clone o repositório

```bash
git clone https://github.com/guilherme-bezerra-castro/APIAcademia.git
cd APIAcademia
```

2. Configure a connection string no `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=AcademiaDb;user=root;password=[suasenha]"
}
```

3. Aplique as migrations para criar o banco

```bash
dotnet ef database update
```

4. Execute a aplicação

```bash
dotnet run
```

5. Acesse a documentação interativa no Swagger

```
https://localhost:{porta}/swagger
```
