# Mod10 - API de Funcionários

Este repositório contém uma API ASP.NET Core (.NET 10) para gerenciar funcionários (CRUD) com persistência via EF Core (SQL Server). A API possui autenticação JWT para operações de escrita e testes automatizados com xUnit usando EF InMemory.

Resumo rápido
- Projeto principal (API): 01-Presentation
- Lógica de aplicação: 02-Application
- Persistência / EF Core: 03-Infrastructure
- Entidades e contratos: 04-Domain
- Testes: 05-Tests

Pré-requisitos
- .NET 10 SDK
- Visual Studio 2022/2026 com LocalDB (ou SQL Server) ou dotnet CLI
- (opcional) dotnet-ef para gerar migrações localmente

Passos para rodar localmente

1. Configurar segredo JWT (recomendado)

   Entre no diretório do projeto Presentation e defina a chave secreta com user-secrets:

   cd 01-Presentation
   dotnet user-secrets init
   dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_SECRETA_AQUI"

   Alternativamente defina variável de ambiente (exemplo PowerShell):

   $env:Jwt__Key = "SUA_CHAVE_SECRETA_AQUI"

   Observação: nunca comite a chave em arquivos de configuração. Em produção use Key Vault ou variáveis de ambiente gerenciadas.

2. Rodar a API (Visual Studio ou CLI)

   - Via Visual Studio: abra a solução Mod10.slnx e rode o projeto 01-Presentation.
   - Via CLI:

	 cd C:\\Users\\T-GAMER\\source\\repos\\Mod10\\01-Presentation
	 dotnet run

   Na primeira execução, a API aplica migrações automaticamente (db.Database.Migrate()) e criará o banco LocalDB configurado em appsettings.json.

3. Acessar Swagger

   Após subir a API em ambiente Development, acesse o Swagger UI em:

   https://localhost:{port}/swagger

   O Swagger mostra os endpoints e permite testar as chamadas.

Autenticação (JWT)

- Endpoint de login: POST /api/auth/login
- Corpo (JSON):

  {
	"usuario": "admin",
	"senha": "123456"
  }

- Credenciais padrões para teste: admin / 123456
- Resposta: { token, expires }

Usando o token

- Para chamadas protegidas (POST, PUT, DELETE em /api/funcionarios) adicione o header:

  Authorization: Bearer <token>

Exemplos curl

- Obter token:

  curl -s -X POST https://localhost:{port}/api/auth/login -H "Content-Type: application/json" -d "{\\"usuario\\":\\"admin\\",\\"senha\\":\\"123456\\"}"

- Criar funcionário (exemplo):

  curl -X POST https://localhost:{port}/api/funcionarios -H "Authorization: Bearer <token>" -H "Content-Type: application/json" -d "{\\"nome\\":\\"Joao\\",\\"cargo\\":\\"Dev\\",\\"salario\\":5000,\\"departamento\\":\\"TI\\"}"

Endpoints principais
- POST /api/auth/login — obtém token JWT
- GET /api/funcionarios — lista pública de funcionários
- GET /api/funcionarios/{id} — obtém funcionário por id
- POST /api/funcionarios — cria (requer JWT)
- PUT /api/funcionarios/{id} — atualiza (requer JWT)
- DELETE /api/funcionarios/{id} — deleta (requer JWT)

Testes

- Para rodar os testes unitários/in-memory:

  cd C:\\Users\\T-GAMER\\source\\repos\\Mod10
  dotnet test

Notas sobre testes
- O projeto 05-Tests usa EF Core InMemory com base de dados nomeada por Guid.NewGuid().ToString() para isolar cada teste.

Migrações

- As migrations iniciais foram adicionadas em 03-Infrastructure/Migrations. Se preferir gerar localmente, execute (na máquina com dotnet-ef):

  cd 03-Infrastructure
  dotnet ef migrations add InitialCreate --startup-project ../01-Presentation --output-dir Migrations
  dotnet ef database update --startup-project ../01-Presentation

Boas práticas e recomendações
- Não armazene segredos em arquivos commitados. Use user-secrets para desenvolvimento e Key Vault / variáveis de ambiente em produção.
- Considere implementar logins reais e persistentes (tabela Users) para produção.
- Adicione testes de integração (WebApplicationFactory) para validar endpoints com autenticação.

Contato

Este README foi gerado automaticamente como parte da entrega do exercício Mod10. Para dúvidas ou ajustes, abra uma issue ou modifique o repositório.
