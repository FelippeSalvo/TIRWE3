# FitnessTracker API

API para rastreamento de atividades físicas desenvolvida com ASP.NET Core 8.

## Tecnologias Utilizadas

- **ASP.NET Core 8** - Framework web
- **MongoDB** - Banco de dados NoSQL
- **JWT Authentication** - Autenticação baseada em tokens
- **AutoMapper** - Mapeamento de objetos
- **BCrypt** - Hash de senhas
- **Swagger** - Documentação da API

## Arquitetura

O projeto segue uma arquitetura em camadas:

- **Controllers** - Endpoints da API
- **Services** - Lógica de negócio
- **Repositories** - Acesso a dados
- **Models** - Entidades do domínio
- **DTOs** - Objetos de transferência de dados
- **Data** - Contexto do MongoDB
- **Helpers** - Utilitários (JWT, AutoMapper)
- **Middleware** - Tratamento global de exceções
- **Config** - Configurações (MongoDB, JWT)

## Pré-requisitos

- .NET 8 SDK
- MongoDB (local ou remoto)

## Configuração

1. Certifique-se de que o MongoDB está rodando localmente na porta padrão (27017) ou atualize a connection string no `appsettings.json`.

2. Configure o JWT Secret Key no `appsettings.json` (use uma chave segura em produção).

## Como Executar

1. Navegue até a pasta do projeto:
```bash
cd FitnessTracker
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Execute o projeto:
```bash
dotnet run
```

4. Acesse o Swagger em: `https://localhost:5001/swagger` ou `http://localhost:5000/swagger`

## Endpoints da API

### Autenticação
- `POST /api/auth/register` - Registrar novo usuário
- `POST /api/auth/login` - Fazer login

### Usuários
- `GET /api/users/me` - Obter dados do usuário atual (requer autenticação)

### Treinos
- `GET /api/workouts` - Listar todos os treinos do usuário (requer autenticação)
- `GET /api/workouts/{id}` - Obter treino por ID (requer autenticação)
- `POST /api/workouts` - Criar novo treino (requer autenticação)
- `PUT /api/workouts/{id}` - Atualizar treino (requer autenticação)
- `DELETE /api/workouts/{id}` - Deletar treino (requer autenticação)

## Exemplo de Uso

### 1. Registrar um usuário
```json
POST /api/auth/register
{
  "email": "usuario@example.com",
  "password": "senha123",
  "firstName": "João",
  "lastName": "Silva"
}
```

### 2. Fazer login
```json
POST /api/auth/login
{
  "email": "usuario@example.com",
  "password": "senha123"
}
```

A resposta incluirá um token JWT que deve ser usado nas requisições subsequentes.

### 3. Criar um treino
```json
POST /api/workouts
Authorization: Bearer {seu_token_jwt}
{
  "name": "Corrida Matinal",
  "description": "Corrida de 5km no parque",
  "duration": 30,
  "caloriesBurned": 300,
  "workoutDate": "2026-02-19T08:00:00Z"
}
```

## Estrutura do Projeto

```
FitnessTracker/
├── Controllers/          # Controladores da API
├── Services/            # Serviços de negócio
├── Repositories/        # Repositórios de dados
├── Models/              # Modelos de domínio
├── DTOs/                # Data Transfer Objects
├── Data/                # Contexto do MongoDB
├── Helpers/             # Utilitários
├── Middleware/          # Middlewares customizados
└── Config/              # Configurações
```
