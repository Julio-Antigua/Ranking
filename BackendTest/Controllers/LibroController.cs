using BackendTest.DTOs;
using BackendTest.Entities;
using BackendTest.Helper;
using BackendTest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackendTest.Controllers
{
    [Route("Api/Libro")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroRepository repository;
        public LibroController(ILibroRepository repository)
        {
            this.repository = repository;
        }

        //Las respuestas de los metodos estan creadas bajo una clase Result la cuales seran devolvidas las respuesta por el constructor de este bajo los parametros,
        // del StatusCode, Mansaje de respuesta, Si tiene un error y los datos que este devolvera.

        //Metodo Get para mostrar todos los libros
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Result<LibroDto> Response = default;
            try
            {
                List<LibroDto> datos = await repository.Get();
                Response = new Result<LibroDto>(
                    Convert.ToInt32(datos != null ? HttpStatusCode.OK : HttpStatusCode.NoContent),
                    "",
                    false,
                    datos
                 );
            }
            catch (Exception ex)
            {
                Response = new Result<LibroDto>
                (
                  Convert.ToInt32(HttpStatusCode.BadRequest),
                  ex.Message.ToString(),
                  true,
                  null
               );
            }
            return Ok(Response);
        }

        //Metodo Get por Id del libro para ver un libro en especifico
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetId(int id)
        {
            Result<LibroDto> Response = default;
            try
            {
                List<LibroDto> datos = await repository.GetId(id);
                Response = new Result<LibroDto>(
                    Convert.ToInt32(datos.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                    "",
                    false,
                    datos
                );

                return Ok(Response);

            }
            catch (Exception ex)
            {
                Response = new Result<LibroDto>(
                   Convert.ToInt32(HttpStatusCode.BadRequest),
                   ex.Message.ToString(),
                   true,
                   null
               );
                return NotFound();
            }

        }

        //Metodo Post Para agregar un nuevo libro
        [HttpPost]
        public async Task<IActionResult> Post(LibroDto dto)
        {
            Result<Libro> Response = default;
            try
            {
                List<Libro> datos = await repository.Post(dto);
                Response = new Result<Libro>
                (
                     Convert.ToInt32(datos != null ? HttpStatusCode.Created : HttpStatusCode.NotFound),
                    "",
                    false,
                    datos
                );

            }
            catch (Exception ex)
            {
                Response = new Result<Libro>
                (
                     Convert.ToInt32(HttpStatusCode.BadRequest),
                    ex.Message.ToString(),
                    true,
                    null
                );
            }
            return Ok();
        }


        //Metodo Put para actualizar un libro por id del libro
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(LibroDto usuarioDto)
        {
            Result<Libro> Response = default;
            try
            {

                bool datos = await repository.Update(usuarioDto);
                Response = new Result<Libro>
                (
                    Convert.ToInt32(datos != false ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest),
                    "",
                    false,
                    null
                );
            }
            catch (Exception ex)
            {
                Response = new Result<Libro>
                (
                     Convert.ToInt32(HttpStatusCode.BadRequest),
                     ex.Message.ToString(),
                     true,
                     null
                );
            }
            return NoContent();
        }

        //Metodo Delete para eliminar un libro por id libro
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            Result<Libro> Response = default;
            try
            {

                bool datos = await repository.Delete(id);
                Response = new Result<Libro>
                (
                    Convert.ToInt32(datos != false ? HttpStatusCode.NoContent : HttpStatusCode.NotFound),
                    "",
                    false,
                    null
                );
            }
            catch (Exception ex)
            {
                Response = new Result<Libro>
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
