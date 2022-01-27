namespace Catalog.Host.Models.Requests;

public class AddNoteRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
}