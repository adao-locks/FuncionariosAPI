using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _context;

        public FuncionarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Funcionario funcionario)
        {
            await _context.Funcionarios.AddAsync(funcionario);
        }

        public void Delete(Funcionario funcionario)
        {
            _context.Funcionarios.Remove(funcionario);
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            return await _context.Funcionarios.AsNoTracking().ToListAsync();
        }

        public async Task<Funcionario?> GetByIdAsync(int id)
        {
            return await _context.Funcionarios.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
        }
    }
}
