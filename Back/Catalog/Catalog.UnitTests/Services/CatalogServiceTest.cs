using Catalog.Host.Models.Response;
using Moq;
using Notes.Host.Data.Entities;
using Notes.Host.Models.Dtos;
using Notes.Host.Repositories.Interfaces;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<INoteRepository> _noteRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly Note _testNote = new Note()
    {
        Title = "Test",
        Description = "Test",
        Author = "Test"
    };

    public CatalogServiceTest()
    {
        _noteRepository = new Mock<INoteRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _noteRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetNotesAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 3;
        var testTotalCount = 2;

        var pagingPaginatedItemsSuccess = new PaginatedItems<Note>()
        {
            Data = new List<Note>()
            {
                new Note()
                {
                    Title = "Test",
                    Description = "Test",
                    Author = "Test"
                },
            },
            PagesCount = testTotalCount,
        };

        var noteSuccess = new Note()
        {
            Title = "Test",
            Description = "Test",
            Author = "Test"
        };

        var noteDtoSuccess = new NoteDto()
        {
            Title = "Test",
            Description = "Test",
            Author = "Test"
        };

        _noteRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<NoteDto>(
            It.Is<Note>(i => i.Equals(noteSuccess)))).Returns(noteDtoSuccess);

        // act
        var result = await _catalogService.GetNotesAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.PagesCount.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetNotesAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _noteRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<NoteDto>>)null!);

        // act
        var result = await _catalogService.GetNotesAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        var id = 1;
        var title = "Shopping list";

        var noteSuccess = new Note()
        {
            Id = id,
            Title = title
        };

        var noteDtoSuccess = new NoteDto()
        {
            Id = id,
            Title = title
        };

        _noteRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == id))).ReturnsAsync(noteSuccess);

        _mapper.Setup(s => s.Map<NoteDto>(
            It.Is<Note>(i => i.Equals(noteSuccess)))).Returns(noteDtoSuccess);

        // act
        var result = await _catalogService.GetByIdAsync(id);

        // assert
        result.Should().NotBeNull();
        result?.Title.Should().Be(title);
        result?.Id.Should().Be(id);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        Task<Note>? testResult = null;
        var id = 1000;

        _noteRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == id))).Returns(testResult!);

        // act
        var result = await _catalogService.GetByIdAsync(id);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddNoteAsync_Success()
    {
        // arrange
        var testResult = 1;

        _noteRepository.Setup(s => s.AddNoteAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddNoteAsync(_testNote.Title, _testNote.Description, _testNote.Author);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddNoteAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _noteRepository.Setup(s => s.AddNoteAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddNoteAsync(_testNote.Title, _testNote.Description, _testNote.Author);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateNote_Success()
    {
        // arrange
        var id = 1;
        var testResult = "Success";

        _noteRepository.Setup(s => s.UpdateNoteAsync(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateNoteAsync(id, _testNote.Title, _testNote.Description, _testNote.Author);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateNote_Failed()
    {
        // arrange
        var id = 1;
        string? testResult = null;

        _noteRepository.Setup(s => s.UpdateNoteAsync(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateNoteAsync(id, _testNote.Title, _testNote.Description, _testNote.Author);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task RemoveNote_Success()
    {
        // arrange
        var id = 1;
        var testResult = "Success";

        _noteRepository.Setup(s => s.RemoveNoteAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.RemoveNoteAsync(id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task RemoveNote_Failed()
    {
        // arrange
        var id = 1;
        string? testResult = null;

        _noteRepository.Setup(s => s.RemoveNoteAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.RemoveNoteAsync(id);

        // assert
        result.Should().Be(testResult);
    }
}