namespace Catalog.Host.Models.Requests
{
    public class UpdateNoteRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}
