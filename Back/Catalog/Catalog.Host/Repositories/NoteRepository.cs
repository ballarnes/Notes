using Catalog.Host.Data;
using Notes.Host.Data.Entities;
using Notes.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<NoteRepository> _logger;

    public NoteRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<NoteRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<Note>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.Notes
            .CountAsync();

        var itemsOnPage = await _dbContext.Notes
            .OrderBy(c => c.Id)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        var totalPages = (totalItems / pageSize) + 1;
        if (totalItems % pageSize == 0)
        {
            totalPages = totalItems / pageSize;
        }

        return new PaginatedItems<Note>() { PagesCount = totalPages, Data = itemsOnPage };
    }

    public async Task<Note?> GetByIdAsync(int id)
    {
        var item = await _dbContext.Notes
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (item == null)
        {
            return null;
        }

        return new Note() { Id = item!.Id, Title = item.Title, Description = item.Description, Author = item.Author };
    }

    public async Task<int?> AddNoteAsync(string title, string description, string author)
    {
        var newType = await _dbContext.Notes.AddAsync(new Note
        {
            Title = title,
            Description = description,
            Author = author
        });

        await _dbContext.SaveChangesAsync();

        return newType.Entity.Id;
    }

    public async Task<string?> UpdateNoteAsync(int id, string title, string description, string author)
    {
        var noteFromDb = await _dbContext.Notes.Where(i => i.Id == id).FirstOrDefaultAsync();

        if (noteFromDb == null)
        {
            return null;
        }

        noteFromDb!.Title = title;
        noteFromDb!.Description = description;
        noteFromDb!.Author = author;
        _dbContext.Notes.Update(noteFromDb);

        await _dbContext.SaveChangesAsync();

        return $"Successfully updated. ({DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss")})";
    }

    public async Task<string?> RemoveNoteAsync(int id)
    {
        var note = await _dbContext.Notes.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (note == null)
        {
            return null;
        }

        _dbContext.Notes.Remove(note);

        await _dbContext.SaveChangesAsync();

        return $"Successfully deleted. ({DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss")})";
    }
}