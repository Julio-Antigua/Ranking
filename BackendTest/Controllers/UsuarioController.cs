using BackendTest.DTOs;
using BackendTest.Entities;
using BackendTest.Helper;
using BackendTest.Interfaces;
using BackendTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackendTest.Controllers
{
    [Route("Api/Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly LibrosTestContext context;
        private readonly IUsuarioRepository repository;

        public UsuarioController(LibrosTestContext context, IUsuarioRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }

        //Las respuestas de los metodos estan creadas bajo una clase Result la cuales seran devolvidas las respuesta por el constructor de este bajo los parametros,
        // del StatusCode, Mansaje de respuesta, Si tiene un error y los datos que este devolvera.

        //Metodo Get para mostrar todos los usuarios
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Result<UsuarioDto> Response = default;
            try
            {
                List<UsuarioDto> datos = await repository.Get();
                Response = new Result<UsuarioDto>(
                    Convert.ToInt32(datos != null ? HttpStatusCode.OK : HttpStatusCode.NoContent),
                    "",
                    false,
                    datos
                 );
            }
            catch (Exception ex)
            {
                Response = new Result<UsuarioDto>
                (
                  Convert.ToInt32(HttpStatusCode.BadRequest),
                  ex.Message.ToString(),
                  true,
                  null
               );
            }
            return Ok(Response);
        }

        //Metodo Get por Id del usuario para ver un usuario en especifico
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetId(int id)
        {
            Result<UsuarioDto> Response = default;
            try
            {
                List<UsuarioDto> datos = await repository.GetId(id);
                Response = new Result<UsuarioDto>(
                    Convert.ToInt32(datos.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                    "",
                    false,
                    datos
                );
                return Ok(Response);

            }
            catch (Exception ex)
            {
                Response = new Result<UsuarioDto>(
                   Convert.ToInt32(HttpStatusCode.BadRequest),
                   ex.Message.ToString(),
                   true,
                   null
               );
                return NotFound();
            }
           
        }

        //Metodo Post Para agregar un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> Post(UsuarioDto dto)
        {
            Result<Usuario> Response = default;
            try
            {
                List<Usuario> datos = await repository.Post(dto);
                Response = new Result<Usuario> 
                (
                     Convert.ToInt32(datos != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest),
                    "",
                    false,
                    datos
                );

            }
            catch (Exception ex)
            {
                Response = new Result<Usuario>
                (
                     Convert.ToInt32(HttpStatusCode.BadRequest),
                    ex.Message.ToString(),
                    true,
                    null
                );
            }
            return Ok();
        }

        //Metodo Put para actualizar un usuario por id del usuario
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(UsuarioDto usuarioDto)
        {
            Result<Usuario> Response = default;
            try 
            {
                
                bool datos = await repository.Update(usuarioDto);
                Response = new Result<Usuario> 
                (
                    Convert.ToInt32(datos != false ? HttpStatusCode.NoContent: HttpStatusCode.BadRequest),
                    "",
                    false,
                    null
                );
            } 
            catch (Exception ex)
            {
                Response = new Result<Usuario>
                (
                     Convert.ToInt32(HttpStatusCode.BadRequest),
                     ex.Message.ToString(),
                     true,
                     null
                );
            }
            return NoContent();
        }

        //Metodo Delete para eliminar un usuario por id usuario
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            Result<Usuario> Response = default;
            try
            {

                bool datos = await repository.Delete(id);
                Response = new Result<Usuario>
                (
                    Convert.ToInt32(datos != false ? HttpStatusCode.NoContent : HttpStatusCode.NotFound),
                    "",
                    false,
                    null
                );
            }
            catch (Exception ex)
            {
                Response = new Result<Usuario>
                (
                     Convert.ToInt32(HttpStatusCode.BadRequest),
                     ex.Message.ToString(),
                     true,
                     null
                );
            }
            return NoContent();

        }
    }

   
}
