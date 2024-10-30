# Gerenciador de tarefas
Teste Gerenciador de tarefas

## Introdução
Este projeto é um sistema de gerenciamento de tarefas desenvolvido em C# usando .NET 8.0.

## Requisitos
- .NET 8.0
- SQL Server

## Configuração

### Clonando o Repositório
```bash
git clone <https://github.com/crosscay/gerenciador_tarefas.git>
cd gerenciador_tarefas
```

## Instruções

Para construir e executar a aplicação, siga os passos abaixo:

1. **Executar a criação da base SQL Server:**

```bash
cd TaskManager/TaskManager

dotnet ef database update
```

2. **Depois, levantar a API REST:**
```bash
dotnet build
dotnet run
```

3. **Acessar o Swagger para testar os endpoints.:**
```bash
http://localhost:5086/swagger

https://localhost:7022/swagger

```

## Exemplo de Swagger entpoints

![swagger](https://github.com/user-attachments/assets/dcd090cc-ea46-4338-9156-3e8d7b80a2a7)