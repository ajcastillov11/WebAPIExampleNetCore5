using Microsoft.AspNetCore.Authorization;
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
    public class ProductController : ControllerBase
    {

        private readonly AppDBContext _db;
        public ProductController(AppDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Product>))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "CanAccessProducts")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _db.Product.OrderBy(p => p.ProductName).Include(i => i.Category).ToListAsync());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                Models.Product obj = await _db.Product.Include(i => i.Category).FirstOrDefaultAsync(f => f.ProductId == id);
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
        public async Task<IActionResult> Post([FromBody] Product model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _db.AddAsync(model);
                await _db.SaveChangesAsync();

                return CreatedAtRoute("GetProductById", new { id = model.ProductId }, model);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody] Product model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _db.Update(model);
                _db.SaveChanges();

                return CreatedAtRoute("GetProductById", new { id = model.ProductId }, model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                Product obj = _db.Product.FirstOrDefault(x => x.ProductId == id);
                if (obj != null)
                {
                    _db.Remove(obj);
                    _db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
