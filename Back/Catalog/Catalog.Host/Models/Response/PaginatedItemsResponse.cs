namespace Catalog.Host.Models.Response;

public class PaginatedItemsResponse<T>
{
    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    public int PagesCount { get; init; }

    public IEnumerable<T> Data { get; init; } = null!;
}
