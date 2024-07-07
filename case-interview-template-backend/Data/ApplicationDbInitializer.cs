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
            new Category {name = "Electronics"},
            new Category {name = "Clothing"},
            new Category {name = "Books"}
        };

        foreach (var category in categories)
        {
            context.Categories.Add(category);
        }

        context.SaveChanges();

        var products = new Product[]
        {
            new Product {name = "Laptop", description = "A laptop", price = 1000, categoryId = 1},
            new Product {name = "T-shirt", description = "A t-shirt", price = 20, categoryId = 2},
            new Product {name = "Book", description = "A book", price = 10, categoryId = 3}
        };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        context.SaveChanges();
    }
}