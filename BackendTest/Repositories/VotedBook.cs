using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Repositories
{
    public class VotedBook
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int VotacionesTotales { get; set; }
        public int CantidadMeGusta { get; set; }
        public int CantidadNoGusta { get; set; }
        public List<librosVotados> LibrosVotados { get; set; }
        public List<librosPublicado> LibrosPublicados { get; set; }

    }

    public class librosVotados
    {
        public int IdLibros { get; set; }
        public string NombreLibros { get; set; }
        public int VotoLibros { get; set; }
        public string VotoLibrosText { get; set; }
    }

    public class librosPublicado
    {
        public int IdLibros { get; set; }
        public string NombreLibros { get; set; }
    }
}
