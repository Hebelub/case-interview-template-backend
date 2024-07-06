using case_interview_template_backend.Data;
using case_interview_template_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace case_interview_template_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TemplateController> _logger;

        public TemplateController(ApplicationDbContext context, ILogger<TemplateController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                _logger.LogInformation("Fetching products from database.");
                var products = await _context.Products.ToListAsync();

                if (products == null || products.Count == 0)
                {
                    _logger.LogWarning("No products found in the database.");
                    return NotFound("No products found.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}