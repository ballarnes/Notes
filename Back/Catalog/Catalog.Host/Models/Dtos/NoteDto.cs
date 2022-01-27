#pragma warning disable CS8618
namespace Notes.Host.Models.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
