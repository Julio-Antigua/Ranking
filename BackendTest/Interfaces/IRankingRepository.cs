using BackendTest.DTOs;
using BackendTest.Entities;
using BackendTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Interfaces
{
    public interface IRankingRepository
    {
        Task<List<RankingDto>> Get();
        Task<List<LibroDto>> GetCantidad();
        Task<List<LibroDto>> GetCantidadIdLibro(int id);
        Task<List<VotedBook>> GetCantidadUsuario(int id);
        Task<List<RankingDto>> GetId(int id);
        Task<List<Ranking>> Post(RankingDto ranking);
        Task<bool> Update(RankingDto ranking);
        Task<bool> Delete(int id);
    }
}
