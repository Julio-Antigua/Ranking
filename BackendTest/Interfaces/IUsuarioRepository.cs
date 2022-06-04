using BackendTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.DTOs;

namespace BackendTest.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioDto>> Get();
        Task<List<UsuarioDto>> GetId(int id);
        Task<List<Usuario>> Post(UsuarioDto usuario);
        Task<bool> Update(UsuarioDto usuario);
        Task<bool> Delete(int id);
    }
}
