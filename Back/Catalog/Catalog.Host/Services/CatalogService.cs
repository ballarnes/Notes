using Catalog.Host.Data;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Notes.Host.Models.Dtos;
using Notes.Host.Repositories.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        INoteRepository noteRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<NoteDto>?> GetNotesAsync(int pageSize, int pageIndex)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _noteRepository.GetByPageAsync(pageIndex, pageSize);

            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<NoteDto>()
            {
                PagesCount = result.PagesCount,
                Data = result.Data.Select(s => _mapper.Map<NoteDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<NoteDto> GetByIdAsync(int id)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _noteRepository.GetByIdAsync(id);
            var mapped = _mapper.Map<NoteDto>(result);
            return mapped;
        });
    }

    public Task<int?> AddNoteAsync(string title, string description, string author)
    {
        return ExecuteSafe(() => _noteRepository.AddNoteAsync(title, description, author));
    }

    public Task<string?> UpdateNoteAsync(int id, string title, string description, string author)
    {
        return ExecuteSafe(() => _noteRepository.UpdateNoteAsync(id, title, description, author));
    }

    public Task<string?> RemoveNoteAsync(int id)
    {
        return ExecuteSafe(() => _noteRepository.RemoveNoteAsync(id));
    }
}