using BackendTest.DTOs;
using BackendTest.Entities;
using BackendTest.Interfaces;
using BackendTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackendTest.Helper;

namespace BackendTest.Controllers
{
    [Route("Api/Ranking")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly IRankingRepository repository;

        public RankingController(IRankingRepository repository)
        {
            this.repository = repository;
        }

        //Las respuestas de los metodos estan creadas bajo una clase Result la cuales seran devolvidas las respuesta por el constructor de este bajo los parametros,
        // del StatusCode, Mansaje de respuesta, Si tiene un error y los datos que este devolvera.

        //Metodo Get Mata Mostrar todos los votos de los libros
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Result<RankingDto> Response = default;
            try
            {
                List<RankingDto> datos = await repository.Get();
                Response = new Result<RankingDto>(
                    Convert.ToInt32(datos != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                    "",
                    false,
                    datos
                 );
            }
            catch (Exception ex)
            {
                Response = new Result<RankingDto>
                (
                  Convert.ToInt32(HttpStatusCode.BadRequest),
                  ex.Message.ToString(),
                  true,
                  null
               );
            }
            return Ok(Response);
        }

        //Metodo Get Cantidad para mostrar los votos detallados por libro
        [HttpGet("Cantidad")]
        public async Task<IActionResult> GetCantidad()
        {
            Result<LibroDto> Response = default;
            try
            {
                List<LibroDto> datos = await repository.GetCantidad();
                Response = new Result<LibroDto>(
                    Convert.ToInt32(datos.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound),
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

        //Metodo Get Por Id Libro para ver los votos detallados de un solo libro
        [HttpGet("Cantidad/Libro/{id:int}")]
        public async Task<IActionResult> GetCantidadIdLibro(int id)
        {
            Result<LibroDto> Response = default;
            try
            {
                List<LibroDto> datos = await repository.GetCantidadIdLibro(id);
                Response = new Result<LibroDto>(
                    Convert.ToInt32(datos.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound),
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

        //Metodo Get por Id del usuario para ver los votos que el usuario a dado a los libros detallada mente y los libros que a publicado
        [HttpGet("Cantidad/Usuario/{id:int}")]
        public async Task<IActionResult> GetCantidadUsuario(int id)
        {
            Result<VotedBook> Response = default;
            try
            {
                List<VotedBook> datos = await repository.GetCantidadUsuario(id);
                Response = new Result<VotedBook>(
                    Convert.ToInt32(datos.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                    "",
                    false,
                    datos
                 );
            }
            catch (Exception ex)
            {
                Response = new Result<VotedBook>
                (
                  Convert.ToInt32(HttpStatusCode.BadRequest),
                  ex.Message.ToString(),
                  true,
                  null
               );
            }
            return Ok(Response);
        }

        //Metodo Get por Id del ranking para ver un voto en especifico
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetId(int id)
        {
            Result<RankingDto> Response = default;
            try
            {
                List<RankingDto> datos = await repository.GetId(id);
                Response = new Result<RankingDto>(
                    Convert.ToInt32(datos.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                    "",
                    false,
                    datos
                );

                return Ok(Response);

            }
            catch (Exception ex)
            {
                Response = new Result<RankingDto>(
                   Convert.ToInt32(HttpStatusCode.BadRequest),
                   ex.Message.ToString(),
                   true,
                   null
               );
                return NotFound();
            }

        }

        //Metodo Post Para agregar un nuevo voto 
        [HttpPost]
        public async Task<IActionResult> Post(RankingDto dto)
        {
            Result<Ranking> Response = default;
            try
            {
                List<Ranking> datos = await repository.Post(dto);
                Response = new Result<Ranking>
                (
                     Convert.ToInt32(datos != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest),
                    "",
                    false,
                    datos
                );

            }
            catch (Exception ex)
            {
                Response = new Result<Ranking>
                (
                     Convert.ToInt32(HttpStatusCode.BadRequest),
                    ex.Message.ToString(),
                    true,
                    null
                );
            }
            return Ok();
        }

        //Metodo Put para actualizar un voto por id del ranking
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(RankingDto rankingDto)
        {
            Result<Ranking> Response = default;
            try
            {

                bool datos = await repository.Update(rankingDto);
                Response = new Result<Ranking>
                (
                    Convert.ToInt32(datos != false ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest),
                    "",
                    false,
                    null
                );
            }
            catch (Exception ex)
            {
                Response = new Result<Ranking>
                (
                     Convert.ToInt32(HttpStatusCode.BadRequest),
                     ex.Message.ToString(),
                     true,
                     null
                );
            }
            return NoContent();
        }

        //Metodo Delete para eliminar un voto por id ranking
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            Result<Ranking> Response = default;
            try
            {

                bool datos = await repository.Delete(id);
                Response = new Result<Ranking>
                (
                    Convert.ToInt32(datos != false ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest),
                    "",
                    false,
                    null
                );
            }
            catch (Exception ex)
            {
                Response = new Result<Ranking>
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
