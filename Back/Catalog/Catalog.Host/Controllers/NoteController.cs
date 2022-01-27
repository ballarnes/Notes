using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Notes.Host.Models.Dtos;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class NoteController : ControllerBase
{
    private readonly ILogger<NoteController> _logger;
    private readonly ICatalogService _catalogService;

    public NoteController(
        ILogger<NoteController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<NoteDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetNotes(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetNotesAsync(request.PageSize, request.PageIndex);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NoteDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetById([FromBody]GetByIdRequest request)
    {
        var result = await _catalogService.GetByIdAsync(request.Id);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddResponse<int?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddNote(AddNoteRequest request)
    {
        var result = await _catalogService.AddNoteAsync(request.Title!, request.Description!, request.Author!);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(new AddResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(EditResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateNote(UpdateNoteRequest request)
    {
        var result = await _catalogService.UpdateNoteAsync(request.Id, request.Title, request.Description, request.Author);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(new EditResponse() { Result = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(EditResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RemoveNote(GetByIdRequest request)
    {
        var result = await _catalogService.RemoveNoteAsync(request.Id);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(new EditResponse() { Result = result });
    }
}