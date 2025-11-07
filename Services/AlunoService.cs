using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NousPainelAPI.Data;
using NousPainelAPI.Domain;
using NousPainelAPI.DTOs;

namespace NousPainelAPI.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _db;
        public AlunoService(AppDbContext db) => _db = db;

        public async Task<Aluno> CreateAsync(AlunoCreateDto dto)
        {
            var a = new Aluno { Nome = dto.Nome, Email = dto.Email };
            _db.Alunos.Add(a);
            await _db.SaveChangesAsync();
            return a;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var a = await _db.Alunos.FindAsync(id);
            if (a == null) return false;
            _db.Alunos.Remove(a);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Aluno>> GetAllAsync()
            => await _db.Alunos.Include(x => x.Checkins).ToListAsync();

        public async Task<Aluno?> GetByIdAsync(int id)
            => await _db.Alunos.Include(x => x.Checkins).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<bool> UpdateAsync(int id, AlunoCreateDto dto)
        {
            var a = await _db.Alunos.FindAsync(id);
            if (a == null) return false;
            a.Nome = dto.Nome ?? a.Nome;
            a.Email = dto.Email ?? a.Email;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddCheckinAsync(int id, CheckinCreateDto dto)
        {
            var a = await _db.Alunos.FindAsync(id);
            if (a == null) return false;
            var c = new Checkin { MoodScore = dto.MoodScore, Observacao = dto.Observacao, AlunoId = id };
            _db.Checkins.Add(c);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
