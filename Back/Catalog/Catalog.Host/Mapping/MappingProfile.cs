using Notes.Host.Data.Entities;
using Notes.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Note, NoteDto>();
    }
}