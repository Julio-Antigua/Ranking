using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuarios { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MeGusta { get; set; }
        public int NoGusta { get; set; }
        public int VotacionesTotales { get; set; }
        public List<LibroDto> Libros { get; set; }
        public List<LibroDto> Publicados { get; set; }
    }
}
