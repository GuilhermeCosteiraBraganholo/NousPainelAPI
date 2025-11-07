using System;

namespace NousPainelAPI.Domain
{
    public class Checkin
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public int MoodScore { get; set; } // 0-10
        public string? Observacao { get; set; }

        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
    }
}
