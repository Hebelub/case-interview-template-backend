using case_interview_template_backend.Models;

namespace case_interview_template_backend.Data;

public class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Cathegories.Any())
        {
            return;
        }

        var cathegories = new Cathegory[]
        {
            new Cathegory {name = "Electronics"},
            new Cathegory {name = "Clothing"},
            new Cathegory {name = "Books"}
        };

        foreach (var cathegory in cathegories)
        {
            context.Cathegories.Add(cathegory);
        }

        context.SaveChanges();

        var products = new Product[]
        {
            new Product {name = "Laptop", description = "A laptop", price = 1000, cathegoryId = 1},
            new Product {name = "T-shirt", description = "A t-shirt", price = 20, cathegoryId = 2},
            new Product {name = "Book", description = "A book", price = 10, cathegoryId = 3}
        };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        context.SaveChanges();
    }
}