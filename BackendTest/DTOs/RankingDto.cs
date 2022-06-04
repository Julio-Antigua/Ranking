using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.DTOs
{
    public class RankingDto
    {
        public int IdRanking { get; set; }
        public int Votaciones { get; set; }
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
    }
}
