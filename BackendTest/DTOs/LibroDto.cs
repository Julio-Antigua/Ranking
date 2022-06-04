using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.DTOs
{
    public class LibroDto
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Autor { get; set; }
        public int UsuarioId { get; set; }
        public int? MeGusta { get; set; }
        public int? NoGusta { get; set; }
        public int? VotacionesTotales { get; set; }

    }
}
