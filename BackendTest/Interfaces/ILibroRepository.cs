using BackendTest.DTOs;
using BackendTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Interfaces
{
    public interface ILibroRepository
    {
        Task<List<LibroDto>> Get();
        Task<List<LibroDto>> GetId(int id);
        Task<List<Libro>> Post(LibroDto libro);
        Task<bool> Update(LibroDto libro);
        Task<bool> Delete(int id);
    }
}
