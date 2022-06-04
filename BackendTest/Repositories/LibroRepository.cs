using BackendTest.DTOs;
using BackendTest.Entities;
using BackendTest.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly LibrosTestContext context;
        public LibroRepository(LibrosTestContext context)
        {
            this.context = context;
        }

        //--------------Metodos Para  EdnPoint--------------//

        //Metodo para mostrar todos los Libros
        public async Task<List<LibroDto>> Get()
        {
            List<Libro> Libro = await context.Libros.ToListAsync();
            List<LibroDto> Dto = Libro.Count > 0 ? CastLibroToDtoList(Libro) : new List<LibroDto>();
            return Dto;
        }

        //Metodo para mostrar un usuario por id
        public async Task<List<LibroDto>> GetId(int id)
        {
            List<Libro> list = new List<Libro>();
            List<LibroDto> Dto = new List<LibroDto>();
            Libro libro = await context.Libros.FirstOrDefaultAsync(x => x.IdLibro == id);
            list.Add(libro);
            if (libro != null) Dto = CastLibroToDtoList(list);
            return Dto;
        }

        //Metodo para agregar un usuario
        public async Task<List<Libro>> Post(LibroDto libroDto)
        {
            List<Libro> list = new List<Libro>();
            Libro libro = CastDtoToLibro(libroDto);
            list.Add(libro);
            context.Libros.Add(libro);
            await context.SaveChangesAsync();
            return list;
        }

        //Metodo para actualizar un usuario por el id del usuario
        public async Task<bool> Update(LibroDto libroDto)
        {
            Libro Data = context.Libros.FirstOrDefault(x => x.IdLibro == libroDto.IdLibro);
            Libro Update = CastUpdate(Data, libroDto);
            context.Libros.Update(Update);

            int rows = await context.SaveChangesAsync();
            return rows > 0;
        }


        //Metodo para eliminar un usuario por el id del usuario
        public async Task<bool> Delete(int id)
        {
            Libro Libro = await context.Libros.FirstOrDefaultAsync(x => x.IdLibro == id);
            context.Libros.Remove(Libro);

            int rows = await context.SaveChangesAsync();
            return rows > 0;
        }


        //---------------   Metodos Para Casteo ------------------------------//


        //Metodo para castear de entidades Libros a entidades LibroDto
        private List<LibroDto> CastLibroToDtoList(List<Libro> libros)
        {
            List<LibroDto> LibroList = libros.Count > 0 ? libros.Select(libros => new LibroDto
            {
                IdLibro = libros.IdLibro,
                Titulo = libros.Titulo,
                Descripcion = libros.Descripcion,
                FechaPublicacion = libros.FechaPublicacion,
                Autor = libros.Autor,
                UsuarioId = libros.UsuarioId
            }).ToList() : default;

            return LibroList;
        }



        //Metodo para castear de entidades LibroDto a entidades Libros

        private Libro CastDtoToLibro(LibroDto dto)
        {
            Libro LibroList = dto == null ? new Libro() : new Libro
            {
                IdLibro = dto.IdLibro,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                FechaPublicacion = dto.FechaPublicacion,
                Autor = dto.Autor,
                UsuarioId = dto.UsuarioId
            };

            return LibroList;
        }

        //Metodo para actualizar los valores de los cuales resive por parametro data de tipo Libro y de Tipo LibroDto libro los cuales contienen los valores nuevos
        private Libro CastUpdate(Libro data, LibroDto libro)
        {
            data.IdLibro = libro.IdLibro;
            data.Titulo = libro.Titulo;
            data.Descripcion = libro.Descripcion;
            data.FechaPublicacion = libro.FechaPublicacion;
            data.Autor = libro.Autor;
            data.UsuarioId = libro.UsuarioId;

            return data;
        }
    }
}
