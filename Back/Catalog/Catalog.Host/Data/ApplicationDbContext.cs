using Catalog.Host.Data.EntityConfigurations;
using Notes.Host.Data.Entities;

namespace Catalog.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Note> Notes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new NoteEntityTypeConfiguration());
    }
}
