using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _repository;

        public FuncionarioService(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<FuncionarioOutputDto> CreateAsync(FuncionarioInputDto dto)
        {
            var entity = new Funcionario
            {
                Nome = dto.Nome,
                Cargo = dto.Cargo,
                Salario = dto.Salario,
                Departamento = dto.Departamento,
                Ativo = true
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return new FuncionarioOutputDto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Cargo = entity.Cargo,
                Salario = entity.Salario,
                Departamento = entity.Departamento,
                Ativo = entity.Ativo
            };
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<FuncionarioOutputDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(e => new FuncionarioOutputDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Cargo = e.Cargo,
                Salario = e.Salario,
                Departamento = e.Departamento,
                Ativo = e.Ativo
            });
        }

        public async Task<FuncionarioOutputDto?> GetByIdAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;
            return new FuncionarioOutputDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Cargo = e.Cargo,
                Salario = e.Salario,
                Departamento = e.Departamento,
                Ativo = e.Ativo
            };
        }

        public async Task UpdateAsync(int id, FuncionarioInputDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;
            entity.Nome = dto.Nome;
            entity.Cargo = dto.Cargo;
            entity.Salario = dto.Salario;
            entity.Departamento = dto.Departamento;
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
