namespace case_interview_template_backend.Models;

public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Booking> Bookings { get; set; }
}