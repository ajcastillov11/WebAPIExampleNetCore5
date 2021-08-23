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
    public class CategoriaController : ControllerBase
    {

        /// <summary>
        /// The database
        /// </summary>
        private readonly AppDBContext _db;
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriaController"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public CategoriaController(AppDBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets the categorias.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Categoria>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                return Ok(await _db.Categorias.OrderBy(x => x.Nombre).ToListAsync());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        [HttpGet("{Id:int}", Name = "GetCategoria")]
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategoria(int Id)
        {
            try
            {
                Models.Categoria obj = await _db.Categorias.FirstOrDefaultAsync(x => x.Id == Id);

                if (obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _db.AddAsync(model);
                await _db.SaveChangesAsync();

                return CreatedAtRoute("GetCategoria", new { Id = model.Id }, model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
