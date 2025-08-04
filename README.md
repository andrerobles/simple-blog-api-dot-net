# Projeto API Dotnet Blog

Esse projeto contém uma API simples de Blog, onde é possível um usuário criar seu acesso para poder criar posts e comentários. Usuários não logados podem listar todos os posts 

### 💻 Pré-requisitos

-   [x] Dotnet Core 8
-   [x] Docker instalado
-   [x] Docker Compose instalado

### Estrutura

Em `Docker/docker-compose.yml`: Contem um container simples para poder utilizar o Postgres como banco de dados

### 🚀 Instalação / Configuração

#### Execute os seguintes comandos em ordem:

#### Criação do container Postgres

```bash
cd Docker
```

```bash
docker-compose up -d
```

#### Voltar ao diretório raiz e executar os comandos

Para criar as tabelas necessárias

```bash
dotnet ef database update
```

Para compilar e rodar o projeto
```bash
dotnet dotnet run
```

### 🚪 Acessos:

-   API: `http://localhost:5090`
-   Swagger: `http://localhost:5090/swagger`
-   Postgres: `localhost:5432`


### Informações

O projeto contém um swagger com todos os end-points listados, mas segue uma descrição de algumas delas:

- `POST /api/user/register` => Registrar usuário
- `POST /api/user/login` => Logar com o usuário que foi criado
- `GET /api/post` => Visualizar todos os posts criados (Não é necessário estar logado)
- `PUT api/post/{id}` => Atualizar post do usuário
- `DELETE /api/post/{id}` => Deleta post do usuário
- `ws://localhost:5090/ws/posts` => Atualiza os posts em tempo real

Importante:

Todas as rotas autenticadas devem utilizar o token gerado pelo end-point `login`

Ex:

`Authorization: Bearer TOKEN_OBTIDO`