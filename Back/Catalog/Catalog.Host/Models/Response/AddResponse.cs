namespace Catalog.Host.Models.Response;

public class AddResponse<T>
{
    public T Id { get; set; } = default(T) !;
}