using case_interview_template_backend.Models;

namespace case_interview_template_backend.Data;

public class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (context.Categories.Any())
        {
            return;
        }

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

        var products = new Product[]
        {
            new Product {Name = "Laptop", Description = "A laptop", Price = 1000, CategoryId = 1},
            new Product {Name = "T-shirt", Description = "A t-shirt", Price = 20, CategoryId = 2},
            new Product {Name = "Book", Description = "A book", Price = 10, CategoryId = 3}
        };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        context.SaveChanges();
    }
}