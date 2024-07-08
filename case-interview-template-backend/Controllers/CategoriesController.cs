using case_interview_template_backend.Data;
using case_interview_template_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace case_interview_template_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var products = await _context.Categories.ToListAsync();
            return Ok(products);
        }

        // GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var product = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Categories.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Categories
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound();
            }

            _context.Categories.RemoveRange(categories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}