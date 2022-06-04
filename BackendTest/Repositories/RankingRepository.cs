using BackendTest.DTOs;
using BackendTest.Entities;
using BackendTest.Helper;
using BackendTest.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Repositories
{
    public class RankingRepository : IRankingRepository
    {
        private readonly LibrosTestContext context;

        public RankingRepository(LibrosTestContext context)
        {
            this.context = context;
        }

        //--------------Metodos Para  EdnPoint--------------//

        //Metodo para mostrar todos los votos enviados a los libros
        public async Task<List<RankingDto>> Get()
        {
            List<Ranking> Rank = await context.Rankings.ToListAsync();
            List<RankingDto> DTO = Rank.Count > 0 ? CastRankingToDtoList(Rank) : new List<RankingDto>();
            return DTO;
        }

        //Metodo para mostrar la cantidad de votos detalladamente por libro
        public async Task<List<LibroDto>> GetCantidad()
        {
            List<Libro> Libros = await context.Libros.ToListAsync();
            List<Ranking> Rankings = await context.Rankings.ToListAsync();
            List<LibroDto> LibrosList = Libros.Count > 0 ? Libros.Select(Lib => new LibroDto
            {
                IdLibro = Lib.IdLibro,
                Titulo = Lib.Titulo,
                Descripcion = Lib.Descripcion,
                FechaPublicacion = Lib.FechaPublicacion,
                Autor = Lib.Autor,
                UsuarioId = Lib.UsuarioId,
                MeGusta = Rankings.Where(x => x.Votaciones == 1 && x.LibroId == Lib.IdLibro).Count(),
                NoGusta = Rankings.Where(x => x.Votaciones == -1 && x.LibroId == Lib.IdLibro).Count(),
                VotacionesTotales = Rankings.Where(x => x.LibroId == Lib.IdLibro).Count()

            }).ToList() : default;

            return LibrosList;
        }

        //Metodo para mostrar la cantidad de votos del libro por Id libro 
        public async Task<List<LibroDto>> GetCantidadIdLibro(int id)
        {
            List<LibroDto> LibrosList = new List<LibroDto>();
            Libro Libros = await context.Libros.FirstOrDefaultAsync(x => x.IdLibro == id);
            List<Ranking> Rankings = await context.Rankings.ToListAsync();

            if (Libros != null)
                LibrosList.Add(new LibroDto
                {
                    IdLibro = Libros.IdLibro,
                    Titulo = Libros.Titulo,
                    Descripcion = Libros.Descripcion,
                    FechaPublicacion = Libros.FechaPublicacion,
                    Autor = Libros.Autor,
                    UsuarioId = Libros.UsuarioId,
                    MeGusta = Rankings.Where(x => x.Votaciones == 1 && x.LibroId == id).Count(),
                    NoGusta = Rankings.Where(x => x.Votaciones == -1 && x.LibroId == id).Count(),
                    VotacionesTotales = Rankings.Where(x => x.LibroId == id).Count()

                });

            return LibrosList;
        }


        //Metodo para mostrar la cantidad de votos del usuario por Id usuario 
        public async Task<List<VotedBook>> GetCantidadUsuario(int id)
        {
            List<VotedBook> UsuarioList = new List<VotedBook>();
            Usuario Usuarios = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuarios == id);
            List<Ranking> Rankings = await context.Rankings.Where(x => x.UsuarioId == id).ToListAsync();
            List<Libro> LibroList = await context.Libros.Where(x => x.UsuarioId == id).ToListAsync();
            List<Libro> LibroListVoted = (from lb in context.Libros.AsEnumerable()
                                          where Rankings.Any(rq => rq.LibroId == lb.IdLibro && rq.UsuarioId == id)
                                          select lb).ToList();
                         
            if (Usuarios != null)
                UsuarioList.Add(new VotedBook
                {
                    IdUsuario = Usuarios.IdUsuarios,
                    NombreUsuario = Usuarios.Nombre,
                    VotacionesTotales = Rankings.Count(),
                    CantidadMeGusta = Rankings.Where(x => x.Votaciones == 1).Count(),
                    CantidadNoGusta = Rankings.Where(x => x.Votaciones == -1).Count(),
                    LibrosPublicados = CastToPublicBook(LibroList),
                    LibrosVotados = CastToVotedBook(LibroListVoted, Rankings)
                });

            return UsuarioList;
        }

        //Metodo para mostrar un Ranking por Id del libro
        public async Task<List<RankingDto>> GetId(int id)
        {
            List<Ranking> list = new List<Ranking>();
            List<RankingDto> DTO = new List<RankingDto>();
            Ranking Rank = await context.Rankings.FirstOrDefaultAsync(x => x.IdRanking == id);
            list.Add(Rank);
            if (Rank != null)
                DTO = CastRankingToDtoList(list);
            return DTO;
        }

        //Metodo para agregar un Ranking
        public async Task<List<Ranking>> Post(RankingDto rankingDto)
        {
            List<Ranking> list = new List<Ranking>();
            Ranking data = context.Rankings.FirstOrDefault(x => x.IdRanking == rankingDto.IdRanking && x.UsuarioId == rankingDto.UsuarioId);
            Ranking Rank = CastDtoToRanking(rankingDto);
            list.Add(Rank);
            context.Rankings.Add(Rank);
            await context.SaveChangesAsync();
            return list;
        }

        //Metodo para actualizar un ranking
        public async Task<bool> Update(RankingDto rankingDto)
        {
            Ranking data = context.Rankings.FirstOrDefault(x => x.IdRanking == rankingDto.IdRanking);
            Ranking Update = CastUpdate(data, rankingDto);
            context.Rankings.Update(Update);
            int rows = await context.SaveChangesAsync();
            return rows > 0;
        }

        //Metodo para eliminar un Ranking
        public async Task<bool> Delete(int id)
        {
            Ranking Rank = await context.Rankings.FirstOrDefaultAsync(x => x.IdRanking == id);
            context.Rankings.Remove(Rank);
            int rows = await context.SaveChangesAsync();
            return rows > 0;
        }

        //---------------   Metodos Para Casteo ------------------------------//


        //Casteo de Ranking a RankingDto
        private List<RankingDto> CastRankingToDtoList(List<Ranking> rankings)
        {
            List<RankingDto> RankList = rankings.Count > 0 ? rankings.Select(rankings => new RankingDto
            {
                IdRanking = rankings.IdRanking,
                Votaciones = rankings.Votaciones,
                LibroId = rankings.LibroId,
                UsuarioId = rankings.UsuarioId,

            }).ToList() : default;
            return RankList;
        }



        //Metodo para castear de RankingDto a Ranking
        private Ranking CastDtoToRanking(RankingDto dto)
        {
            Ranking RankList = dto == null ? new Ranking() : new Ranking
            {
                IdRanking = dto.IdRanking,
                Votaciones = dto.Votaciones,
                LibroId = dto.LibroId,
                UsuarioId = dto.UsuarioId,
            };
            return RankList;
        }

        //Metodo para Actualizar los valores de los cuales resive por parametro Ranking y RankingDto los cuales contienen los valores nuevos
        private Ranking CastUpdate(Ranking data, RankingDto ranking)
        {
            data.IdRanking = ranking.IdRanking;
            data.Votaciones = ranking.Votaciones;
            data.LibroId = ranking.LibroId;
            data.UsuarioId = ranking.UsuarioId;

            return data;
        }

        //Metodo para buscar todos los libros votados por el usuario, detallando los votos que el ah ingresado
        private List<librosVotados> CastToVotedBook(List<Libro> LibroList = default, List<Ranking> Rankings = default)
        {
            List<librosVotados> Libros = LibroList != null ? LibroList.Select(votos => new librosVotados
            {
                IdLibros = votos.IdLibro,
                NombreLibros = votos.Titulo,
                VotoLibros = Rankings.FirstOrDefault(o => o.LibroId == votos.IdLibro).Votaciones,
                VotoLibrosText = Rankings.FirstOrDefault(o => o.LibroId == votos.IdLibro).Votaciones == 1 ? MensajeRespuesta.Me_Gusta : MensajeRespuesta.No_Me_Gusta
            }).ToList() : default;
            return Libros;
        }


        //Metodo para buscar todos los libros publicados por el usuario, el cual recibe por parametro una lista de Ranking y el id del usuario,
        //para retornar una lista de librosPublicado
        private List<librosPublicado> CastToPublicBook(List<Libro> LibroList = default)
        {
            List<librosPublicado> Libros = LibroList != null ? LibroList.Select(votos => new librosPublicado
            {
                IdLibros = votos.IdLibro,
                NombreLibros = votos.Titulo
            }).ToList() : default;
            return Libros;
        }






    }
}
