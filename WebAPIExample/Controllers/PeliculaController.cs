using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIExample.Data;
using WebAPIExample.Models;

namespace WebAPIExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {

        private readonly AppDBContext _db;
        public PeliculaController(AppDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Pelicula>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPeliculas()
        {
            try
            {
                return Ok(await _db.Peliculas.OrderBy(x => x.NombrePelicula).Include(x => x.Categoria).ToListAsync());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        [HttpGet("{id:int}", Name = "GetPelicula")]
        [ProducesResponseType(200, Type = typeof(Pelicula))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPelicula(int id)
        {
            try
            {
                var obj = await _db.Peliculas.Include(p => p.Categoria).FirstOrDefaultAsync(x => x.Id == id);

                return obj == null ? NotFound() : Ok(obj);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the pelicula.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddPelicula([FromBody] Pelicula model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _db.AddAsync(model);
                await _db.SaveChangesAsync();

                return CreatedAtRoute("GetPelicula", new { id = model.Id }, model);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
