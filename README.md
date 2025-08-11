# CRUD de Tarefas – C# .NET

API REST para gerenciamento de tarefas com CRUD completo (criar, listar, detalhar, atualizar e deletar), camadas **Controller → Service → Repository**, validações e persistência em **SQL Server**.


## ✅ Requisitos
- .NET 8+
- SQL Server (local ou remoto)

## ⚙️ Configuração (appsettings.json)
Edite **appsettings.json** e ajuste a connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=CrudTarefasDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

**Opções comuns de servidor:**
- `Server=.;` → usa o **SQL Server padrão** da máquina (se houver).
- Se usar certificado self-signed, mantenha `TrustServerCertificate=True`.

> Dica: se estiver no VS 2022 com LocalDB, uma string que costuma “pegar de primeira”:

## 🗃️ Criar o banco e as tabelas (Migrations)

### **Package Manager Console** (Visual Studio)
No **Visual Studio**: `Tools > NuGet Package Manager > Package Manager Console` e rode:

```powershell
Add-Migration CriarTabelaTarefas
Update-Database
```
## ▶️ Como rodar
```bash
dotnet run
```

## 🔗 Coleção Postman
Você pode importar esta coleção para testar os endpoints:
```
https://t2g2renato-8887760.postman.co/workspace/Personal-Workspace~93d2813a-e2e8-406b-b6ba-bc2f9c056132/collection/47470570-447537f7-7958-49a4-a0be-3c1d7ce8d8a9?action=share&creator=47470570
```

## 📚 Endpoints (cURL)

> Troque a porta se necessário (5001 é um exemplo).

### Criar tarefa (POST /api/tarefa)
```bash
curl -X POST https://localhost:5001/api/tarefa   -H "Content-Type: application/json"   -d '{"titulo":"Pagar contas","descricao":"Luz e água","dataVencimento":"2025-11-12T00:00:00","status":0}'
```

### Listar tarefas (GET /api/tarefa)
```bash
curl https://localhost:5001/api/tarefa
```

### Detalhar por ID (GET /api/tarefa/{id})
```bash
curl https://localhost:5001/api/tarefa/1
```

### Atualizar (PUT /api/tarefa/{id})
> PUT aceita **parcial** (envie só o que quer mudar).
```bash
curl -X PUT https://localhost:5001/api/tarefa/1   -H "Content-Type: application/json"   -d '{"titulo":"Pagar contas (ajustado)","status":1}'
```

### Deletar (DELETE /api/tarefa/{id})
```bash
curl -X DELETE https://localhost:5001/api/tarefa/1
```

## 🧠 Regras de negócio implementadas
- **Título obrigatório** (`[Required]`, `[StringLength]`).
- **Data de vencimento** deve ser **hoje ou futura**.
- **Status** é um **enum**: `Pendente (0)`, `EmProgresso (1)`, `Concluida (2)`.
- **Trim** de `Título` e `Descrição`.
- **Duplicidade**:
  - **Criar**: bloqueia tarefas com **mesmo Título + mesma Data** (independente de status).
  - **Atualizar**: bloqueia duplicidade **ignorando a própria tarefa** (mesmo Título + Data em outro ID).
- **Tratamento de erros** no Controller (mensagens claras para validação e “não encontrado”).

## 🧩 Estrutura (camadas)
- `Controllers/` → expõe endpoints.
- `Services/` → regras de negócio/validações.
- `Repository/` → acesso a dados (EF Core).
- `Data/AppDbContext.cs` → contexto EF Core.
- `Models/` & `Dto/` → entidades e contratos de entrada/saída.


## 📄 Licença
Uso educacional / teste técnico.
