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
            // Validation logic: Check if the room is available for the specified period
            bool isRoomAvailable = !_context.Bookings
                .Any(b => b.RoomId == booking.RoomId
                          && ((booking.StartDate >= b.StartDate && booking.StartDate < b.EndDate) ||
                              (booking.EndDate > b.StartDate && booking.EndDate <= b.EndDate) ||
                              (booking.StartDate <= b.StartDate && booking.EndDate >= b.EndDate)));

            if (!isRoomAvailable)
            {
                return BadRequest("The room is not available for the selected period.");
            }

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