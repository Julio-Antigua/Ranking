using BackendTest.Entities;
using BackendTest.DTOs;
using BackendTest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Repositories
{
   
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly LibrosTestContext context;

        public UsuarioRepository(LibrosTestContext context)
        {
            this.context = context;
        }

        //--------------Metodos Para  EdnPoint--------------//

        //Metodo para mostrar todos los Usuarios
        public async Task<List<UsuarioDto>> Get()
        {
            List<Usuario> Users = await context.Usuarios.ToListAsync();
            List<UsuarioDto> DTO = Users.Count > 0 ? CastUsuarioToDtoList(Users) : new List<UsuarioDto>();
            return DTO;
        }

        //Metodo para mostrar un usuario por id
        public async Task<List<UsuarioDto>> GetId(int id)
        {
            List<Usuario> list = new List<Usuario>();
            List<UsuarioDto> DTO = new List<UsuarioDto>();
            Usuario User = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuarios == id);
            list.Add(User);
            if (User != null)
                DTO = CastUsuarioToDtoList(list);                        
            return DTO;
        }

        //Metodo para agregar un usuario
        public async Task<List<Usuario>> Post(UsuarioDto usuario)
        {         
            List<Usuario> list = new List<Usuario>();
            Usuario Users = CastDtoToUsuario(usuario);
            list.Add(Users);
            context.Usuarios.Add(Users);
            await context.SaveChangesAsync();
            return list;
        }

        //Metodo para actualizar un usuario por el id del usuario
        public async Task<bool> Update(UsuarioDto usuarioDto)
        {
            Usuario data = context.Usuarios.FirstOrDefault(x => x.IdUsuarios == usuarioDto.IdUsuarios);
            Usuario Update = CastUpdate(data,usuarioDto);
            context.Usuarios.Update(Update);
            int rows =  await context.SaveChangesAsync();
            return rows > 0;
        }

        //Metodo para eliminar un usuario por el id del usuario
        public async Task<bool> Delete(int id)
        {
            Usuario User = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuarios == id);
            context.Usuarios.Remove(User);
            int rows = await context.SaveChangesAsync();
            return rows > 0;
        }


        //---------------   Metodos Para Casteo ------------------------------//

        //Metodo para castear de entidades Usuario a entidades UsuarioDto
        private List<UsuarioDto> CastUsuarioToDtoList(List<Usuario> usuarios)
        {
            List<UsuarioDto> UserList = usuarios.Count > 0 ? usuarios.Select(usuario => new UsuarioDto
            {
                IdUsuarios = usuario.IdUsuarios,
                Nombre = usuario.Nombre,
                Edad = usuario.Edad,
                Email = usuario.Email,
                Password = usuario.Password
            }).ToList() : default;

            return UserList;
        }
       


        //Metodo para castear de entidades UsuarioDta a entidades Usuario
        private Usuario CastDtoToUsuario(UsuarioDto dto)
        {
            Usuario UserList = dto == null ? new Usuario() : new Usuario
            {
                IdUsuarios = dto.IdUsuarios,
                Nombre = dto.Nombre,
                Edad = dto.Edad,
                Email = dto.Email,
                Password = dto.Password
            };

            return UserList;
        }

        //Metodo para actualizar los valores de los cuales resive por parametro Ranking y RankingDto los cuales contienen los valores nuevos
        private Usuario CastUpdate(Usuario data,UsuarioDto usuario)
        {
            data.IdUsuarios = usuario.IdUsuarios;
            data.Nombre = usuario.Nombre;
            data.Edad = usuario.Edad;
            data.Email = usuario.Email;
            data.Password = usuario.Password;
            
            return data;
        }


    }
}
