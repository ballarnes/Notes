using Catalog.Host.Models.Response;
using Notes.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<NoteDto>?> GetNotesAsync(int pageSize, int pageIndex);
    Task<NoteDto> GetByIdAsync(int id);
    Task<int?> AddNoteAsync(string title, string description, string author);
    Task<string?> UpdateNoteAsync(int id, string title, string description, string author);
    Task<string?> RemoveNoteAsync(int id);
}