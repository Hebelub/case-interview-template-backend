using case_interview_template_backend.Data;
using case_interview_template_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace case_interview_template_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<List<Room>>> Get()
        {
            var room = await _context.Rooms.Include(x => x.Category).ToListAsync();
            List<Room> room2;
            
            using (var httpRoomResponse = await _httpClient.GetAsync("https://api.example.com/rooms"))
            {
                httpRoomResponse.EnsureSuccessStatusCode();
                var jsonString = await httpRoomResponse.Content.ReadAsStringAsync();
                if (jsonString == null)
                {
                    room2 = new List<Room>();
                }
                else
                {
                    room2 = JsonSerializer.Deserialize<List<Room>>(jsonString);
                }
            }

            return Ok(room.Concat(room2));
        }

        // GET: api/Rooms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetById(int id)
        {
            var product = await _context.Rooms.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult<Room>> Create(Room product)
        {
            _context.Rooms.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: api/Rooms/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Rooms/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Rooms
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var rooms = await _context.Rooms.ToListAsync();

            if (rooms == null || rooms.Count == 0)
            {
                return NotFound();
            }

            _context.Rooms.RemoveRange(rooms);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}