using System;
using System.Collections.Generic;

#nullable disable

namespace BackendTest.Entities
{
    public partial class Libro
    {
        public Libro()
        {
            Rankings = new HashSet<Ranking>();
        }

        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Autor { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Ranking> Rankings { get; set; }
    }
}
