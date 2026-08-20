using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task<Funcionario?> GetByIdAsync(int id);
        Task AddAsync(Funcionario funcionario);
        void Update(Funcionario funcionario);
        void Delete(Funcionario funcionario);
        Task<int> SaveChangesAsync();
    }
}
