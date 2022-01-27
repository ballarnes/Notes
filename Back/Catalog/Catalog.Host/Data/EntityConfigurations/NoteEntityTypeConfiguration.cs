using Notes.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class NoteEntityTypeConfiguration
    : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Note");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("note_hilo")
            .IsRequired();

        builder.Property(cb => cb.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cb => cb.Description)
            .IsRequired();

        builder.Property(cb => cb.Author)
            .IsRequired()
            .HasMaxLength(100);
    }
}