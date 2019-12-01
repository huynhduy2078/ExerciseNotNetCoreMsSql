using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExerciseOne.Models;

namespace ExerciseOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductItems()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductItemsById(long id)
        {
            var todoItem = await _context.Products.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProducItems(Product item)
        {
            _context.Products.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductItemsById), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProducItem(int id, Product item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProducItem(int id)
        {
            var producItem = _context.Products.Find(id);

            if (producItem == null)
            {
                return NotFound();
            }

            _context.Products.Remove(producItem);
            await _context.SaveChangesAsync();

            return producItem;
        }
    }
}
