using System;
using System.Collections.Generic;

namespace NousPainelAPI.Domain
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public List<Checkin> Checkins { get; set; } = new();
    }
}
