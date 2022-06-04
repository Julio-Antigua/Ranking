using BackendTest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

#nullable disable

namespace BackendTest.Entities
{
    public partial class Usuario 
    {
        public Usuario()
        {
            Libros = new HashSet<Libro>();
            Rankings = new HashSet<Ranking>();
        }

        public int IdUsuarios { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        internal virtual ICollection<Libro> Libros { get; set; }
        internal virtual ICollection<Ranking> Rankings { get; set; }
          
    }
}
