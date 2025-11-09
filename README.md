# NOUS Painel API — Sprint 2 (.NET)

API simples para gestão de alunos e check-ins. Este repositório contém a evolução da Sprint 1 para a Sprint 2,
seguindo exatamente o enunciado do professor.

## O que foi implementado nesta Sprint 2
- **Camada Web API** com CRUD do domínio `Aluno`
- **Rota de search** com **paginação, filtros e ordenação**: `GET /api/aluno/search`
- **HATEOAS** nos retornos do domínio `Aluno`
- **README** atualizado
- **Swagger** habilitado por padrão
- **Banco InMemory** (para facilitar correção e testes)

## Endpoints principais
- `GET /api/aluno` — lista todos os alunos (com `_links`)
- `GET /api/aluno/{id}` — detalhes (com `_links`)
- `POST /api/aluno` — cria aluno
- `PUT /api/aluno/{id}` — atualiza aluno
- `DELETE /api/aluno/{id}` — remove aluno
- `POST /api/aluno/{id}/checkin` — adiciona check-in ao aluno
- `GET /api/aluno/search?nome=...&email=...&sortBy=nome|email&order=asc|desc&page=1&pageSize=10`

## Como executar
1. Requisitos: .NET SDK 9 (ou compatível com este projeto)
2. No terminal, dentro da pasta do projeto:
   ```bash
   dotnet run
   ```
3. Acesse o Swagger:
   - `http://localhost:5129/swagger` (HTTP)
   - `https://localhost:7250/swagger` (HTTPS)

### Observações
- O banco **InMemory** é proposital nesta Sprint 2.
- O HATEOAS aparece nas respostas como o campo `_links` com `self`, `update`, `delete`, `checkin`.
- As telas/clients não fazem parte desta entrega; foco aqui é **.NET Web API** como no enunciado.

---

**Integrantes**
- Guilherme Costeira Braganholo (RM 560628)
- Julio Cesar Dias Vilella (RM 560494)
- Gabriel Nakamura Ogata (RM 560671)

