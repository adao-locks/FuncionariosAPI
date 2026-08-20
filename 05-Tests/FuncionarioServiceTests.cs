using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class FuncionarioServiceTests
    {
        private static AppDbContext CriarContextoDeTeste()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarFuncionariosCadastrados()
        {
            using var context = CriarContextoDeTeste();

                // inserir 2 funcionários diretamente no contexto
                context.Funcionarios.AddRange(new List<Funcionario>
                {
                    new Funcionario { Nome = "A", Cargo = "Dev", Salario = 1000m, Departamento = "TI", Ativo = true },
                    new Funcionario { Nome = "B", Cargo = "Analista", Salario = 2000m, Departamento = "Negócios", Ativo = true }
                });
                context.SaveChanges();

            var repo = new FuncionarioRepository(context);
            var service = new FuncionarioService(repo);

            var all = await service.GetAllAsync();

            Assert.Equal(2, System.Linq.Enumerable.Count(all));
        }

        [Fact]
        public async Task GetByIdAsync_IdInexistente_DeveLancarKeyNotFoundException()
        {
            using var context = CriarContextoDeTeste();

            var repo = new FuncionarioRepository(context);
            var service = new FuncionarioService(repo);

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetByIdAsync(999));
        }

        [Fact]
        public async Task CreateAsync_DeveSalvarERetornarFuncionario()
        {
            using var context = CriarContextoDeTeste();

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

            Assert.True(created.Id > 0);
        }
    }
}
