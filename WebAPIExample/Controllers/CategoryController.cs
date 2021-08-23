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
    public class CategoryController : ControllerBase
    {

        private readonly AppDBContext _db;

        public CategoryController(AppDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Category>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _db.Category.OrderBy(x => x.CategoryName).ToListAsync());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                Category obj = await _db.Category.FirstOrDefaultAsync(i => i.CategoryId == id);
                return obj == null ? NotFound() : Ok(obj);
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
        public async Task<IActionResult> Post([FromBody] Category model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _db.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetCategoryById", new { id = model.CategoryId }, model);
        }

    }
}
