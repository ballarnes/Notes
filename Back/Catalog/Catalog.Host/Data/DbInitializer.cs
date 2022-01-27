using Notes.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Notes.Any())
        {
            await context.Notes.AddRangeAsync(GetPreconfiguredNotesItems());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<Note> GetPreconfiguredNotesItems()
    {
        return new List<Note>()
        {
            new Note() { Title = "Shopping list", Description = "Milk, bread, eggs", Author = "Steve" },
            new Note() { Title = "Films to watch", Description = "Tenet, Dune", Author = "Steve" },
            new Note() { Title = "Birthday gift for Ann", Description = "THE RING SHE SHOWED!!", Author = "Steve" },
            new Note() { Title = "Shopping list", Description = "brown hoodie, white t-shirt", Author = "Steve" },
            new Note() { Title = "Plans for week", Description = "Meet with Ann", Author = "Steve" }
        };
    }
}