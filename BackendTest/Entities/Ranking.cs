using System;
using System.Collections.Generic;

#nullable disable

namespace BackendTest.Entities
{
    public partial class Ranking
    {
        public int IdRanking { get; set; }
        public int Votaciones { get; set; }
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }

        public virtual Libro Libro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
