# APIAcademia

API REST para gerenciamento de alunos e planos de uma academia, desenvolvida como projeto de portfólio. A aplicação cobre o ciclo completo de um backend moderno: autenticação, validação de entrada, tratamento de erros, acesso a dados com repository pattern, operações assíncronas e deploy em produção.

## Tecnologias

- **C# / ASP.NET Core 8** — framework principal
- **Entity Framework Core 8** com **MySQL (Pomelo)** — ORM e banco de dados
- **JWT Bearer** — autenticação stateless
- **FluentValidation** — validação declarativa de entrada
- **Repository Pattern** — separação entre lógica de negócio e acesso a dados
- **Async/Await** — operações de banco completamente assíncronas
- **Middleware** customizado para tratamento centralizado de exceções
- **Action Filter** para logging de requisições
- **Paginação** com Skip/Take no EF Core

## API em Produção

**Base URL:** `https://apiacademia-production.up.railway.app`

> O serviço usa o plano gratuito do Railway. A primeira requisição após um períodode inatividade pode levar alguns segundos enquanto o servidor inicializa. As requisições seguintes respondem normalmente.

## Endpoints

| Método | Rota | Descrição | Autenticação |
|--------|------|-----------|:------------:|
| POST | `/Auth/login` | Gera token JWT | Não |
| GET | `/health` | Status da API | Não |
| GET | `/Alunos` | Lista todos os alunos | Sim |
| GET | `/Alunos/{id}` | Busca aluno por ID | Sim |
| GET | `/Alunos/primeiro` | Retorna o primeiro aluno | Sim |
| GET | `/Alunos/paginado?pagina=1&itensPorPagina=10` | Lista paginada | Sim |
| GET | `/Alunos/filtrar?ativo=true&planoId=1` | Filtra por status e plano | Sim |
| POST | `/Alunos` | Cria novo aluno | Sim |
| PUT | `/Alunos/{id}` | Atualiza aluno completo | Sim |
| PATCH | `/Alunos/{id}/status` | Atualiza status (ativo/inativo) | Sim |
| DELETE | `/Alunos/{id}` | Remove aluno | Sim |
| GET | `/Planos` | Lista todos os planos | Sim |
| GET | `/Planos/{id}` | Busca plano por ID | Sim |
| POST | `/Planos` | Cria novo plano | Sim |
| PUT | `/Planos/{id}` | Atualiza plano | Sim |
| DELETE | `/Planos/{id}` | Remove plano | Sim |

## Como testar

Os endpoints protegidos exigem um token JWT no header `Authorization`. O fluxo é sempre o mesmo: fazer login para obter o token, depois usá-lo nas requisições seguintes.

### Passo 1 — Obter o token

```powershell
# PowerShell
$body = '{"email":"admin@academia.com","senha":"senha123"}'
$response = Invoke-WebRequest -Uri "https://apiacademia-production.up.railway.app/Auth/login" -Method POST -ContentType "application/json" -Body $body -UseBasicParsing
$token = ($response.Content | ConvertFrom-Json).token
```

```bash
# curl (Linux/macOS)
curl -X POST https://apiacademia-production.up.railway.app/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@academia.com","senha":"senha123"}'
```

### Passo 2 — Usar o token nas requisições

```powershell
# PowerShell — listar alunos
Invoke-WebRequest -Uri "https://apiacademia-production.up.railway.app/Alunos" -Headers @{ Authorization = "Bearer $token" } -UseBasicParsing

# PowerShell — buscar aluno por ID
Invoke-WebRequest -Uri "https://apiacademia-production.up.railway.app/Alunos/1" -Headers @{ Authorization = "Bearer $token" } -UseBasicParsing

# PowerShell — listar com paginação
Invoke-WebRequest -Uri "https://apiacademia-production.up.railway.app/Alunos/paginado?pagina=1&itensPorPagina=3" -Headers @{ Authorization = "Bearer $token" } -UseBasicParsing

# PowerShell — filtrar alunos ativos do plano 1
Invoke-WebRequest -Uri "https://apiacademia-production.up.railway.app/Alunos/filtrar?ativo=true&planoId=1" -Headers @{ Authorization = "Bearer $token" } -UseBasicParsing

# PowerShell — atualizar status de um aluno (PATCH)
Invoke-WebRequest -Uri "https://apiacademia-production.up.railway.app/Alunos/1/status" -Method PATCH -ContentType "application/json" -Body "false" -Headers @{ Authorization = "Bearer $token" } -UseBasicParsing
```

```bash
# curl — listar alunos
curl https://apiacademia-production.up.railway.app/Alunos \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"

# curl — buscar aluno por ID
curl https://apiacademia-production.up.railway.app/Alunos/1 \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"
```

### Exemplo de resposta — GET /Alunos/1

```json
{
  "alunoId": 1,
  "nome": "José da Silva",
  "email": "josedasilva@email.com",
  "imagemURL": "josedasilva.jpg",
  "ativo": true,
  "dataNascimento": "1975-08-22T00:00:00",
  "planoId": 1,
  "planoNome": "Saúde"
}
```

### Exemplo de resposta — GET /Alunos/paginado

```json
{
  "totalItens": 6,
  "totalPaginas": 2,
  "paginaAtual": 1,
  "itensPorPagina": 3,
  "dados": [...]
}
```

## Como rodar localmente

**Pré-requisitos:** .NET 8 SDK e MySQL instalado localmente.

**1.** Clone o repositório:

```bash
git clone https://github.com/seu-usuario/APIAcademia.git
cd APIAcademia
```

**2.** Crie o arquivo `APIAcademia/appsettings.Development.json` com suas configurações locais (este arquivo não é commitado):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ApiAcademiaDB;Uid=seu_usuario;Pwd=sua_senha;"
  },
  "Jwt": {
    "SecretKey": "chave-local-com-pelo-menos-32-caracteres",
    "Issuer": "APIAcademia",
    "Audience": "APIAcademiaClientes",
    "ExpiracaoHoras": 8
  }
}
```

**3.** Aplique as migrations e inicie:

```bash
cd APIAcademia
dotnet ef database update
dotnet run
```

O Swagger estará disponível em `https://localhost:7284/swagger`.

---

> Desenvolvido por **Guilherme** — estudante de Análise e Desenvolvimento de Sistemas no IFSP, em busca de estágio ou vaga júnior em desenvolvimento back-end.