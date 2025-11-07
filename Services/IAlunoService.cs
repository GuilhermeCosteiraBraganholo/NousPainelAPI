using System.Collections.Generic;
using System.Threading.Tasks;
using NousPainelAPI.Domain;
using NousPainelAPI.DTOs;

namespace NousPainelAPI.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno?> GetByIdAsync(int id);
        Task<Aluno> CreateAsync(AlunoCreateDto dto);
        Task<bool> UpdateAsync(int id, AlunoCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddCheckinAsync(int id, CheckinCreateDto dto);
    }
}
