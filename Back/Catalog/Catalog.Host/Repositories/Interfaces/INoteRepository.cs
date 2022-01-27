using Catalog.Host.Data;
using Notes.Host.Data.Entities;

namespace Notes.Host.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<PaginatedItems<Note>> GetByPageAsync(int pageIndex, int pageSize);
        Task<Note?> GetByIdAsync(int id);
        Task<int?> AddNoteAsync(string title, string description, string author);
        Task<string?> UpdateNoteAsync(int id, string title, string description, string author);
        Task<string?> RemoveNoteAsync(int id);
    }
}
