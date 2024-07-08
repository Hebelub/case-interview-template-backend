using case_interview_template_backend.Models;

namespace case_interview_template_backend.Data;

public class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (context.Categories.Any())
        {
            return;
        }

        // Seed Categories
        var categories = new Category[]
        {
            new Category {Name = "Electronics"},
            new Category {Name = "Clothing"},
            new Category {Name = "Books"}
        };

        foreach (var category in categories)
        {
            context.Categories.Add(category);
        }

        context.SaveChanges();

        // Seed Users
        var users = new User[]
        {
            new User {FirstName = "John", LastName = "Doe", Email = "john.doe@example.com"},
            new User {FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com"}
        };

        foreach (var user in users)
        {
            context.Users.Add(user);
        }

        context.SaveChanges();

        // Seed Rooms
        var rooms = new Room[]
        {
            new Room {RoomNumber = 101, CategoryId = 1, Status = "Available"},
            new Room {RoomNumber = 102, CategoryId = 2, Status = "Occupied"},
            new Room {RoomNumber = 103, CategoryId = 3, Status = "OutOfService"}
        };

        foreach (var room in rooms)
        {
            context.Rooms.Add(room);
        }

        context.SaveChanges();

        // Seed Bookings
        var bookings = new Booking[]
        {
            new Booking
            {
                RoomId = 1, UserId = 1, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5), Status = "Completed"
            },
            new Booking
            {
                RoomId = 2, UserId = 2, StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(2), Status = "CheckedIn"
            },
            new Booking
            {
                RoomId = 3, UserId = 1, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(6), Status = "Reserved"
            }
        };

        foreach (var booking in bookings)
        {
            context.Bookings.Add(booking);
        }

        context.SaveChanges();
    }
}
