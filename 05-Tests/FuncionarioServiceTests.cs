using Application.DTOs;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class FuncionarioServiceTests
    {
        [Fact]
        public async Task CreateAndGetAll_ReturnsCreatedEntity()
        {
            var dbName = $"TestDb_CreateAndGetAll_{System.Guid.NewGuid():N}";
            var connection = $"Server=(localdb)\\mssqllocaldb;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connection)
                .Options;

            using var context = new AppDbContext(options);
            // garantir banco criado
            context.Database.EnsureCreated();

            var repo = new FuncionarioRepository(context);
            var service = new FuncionarioService(repo);

            var dto = new FuncionarioInputDto
            {
                Nome = "Joao",
                Cargo = "Dev",
                Salario = 5000m,
                Departamento = "TI"
            };

            var created = await service.CreateAsync(dto);

            var all = await service.GetAllAsync();

            Assert.Contains(all, f => f.Id == created.Id && f.Nome == "Joao");

            // cleanup
            context.Database.EnsureDeleted();
        }
    }
}
