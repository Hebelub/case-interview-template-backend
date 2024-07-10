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
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<List<Booking>>> Get()
        {
            var bookings = await _context.Bookings
                .Include(x => x.Room)
                .Include(x => x.User)
                .ToListAsync();
            return Ok(bookings);
        }

        // GET: api/Bookings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(int id)
        {
            var booking = await _context.Bookings
                .Include(x => x.Room)
                .Include(x => x.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // DELETE: api/Bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> Book(Booking booking)
        {
            // Find available room with the specified category for the given period
            var availableRoom = _context.Rooms
                .Include(r => r.Bookings) // Ensure we include bookings related to each room
                .Where(r => booking.Room == null || r.CategoryId == booking.Room.CategoryId)
                .FirstOrDefault(r => r.Bookings.All(b =>
                    (booking.StartDate < b.StartDate || booking.StartDate >= b.EndDate) &&
                    (booking.EndDate <= b.StartDate || booking.EndDate > b.EndDate)));

            if (availableRoom == null)
            {
                return BadRequest("No rooms of the selected category are available for the specified period.");
            }
            
            // Set the RoomId to the available room
            booking.RoomId = availableRoom.Id;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}