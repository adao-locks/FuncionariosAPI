using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioOutputDto>> GetAllAsync();
        Task<FuncionarioOutputDto?> GetByIdAsync(int id);
        Task<FuncionarioOutputDto> CreateAsync(FuncionarioInputDto dto);
        Task UpdateAsync(int id, FuncionarioInputDto dto);
        Task DeleteAsync(int id);
    }
}
