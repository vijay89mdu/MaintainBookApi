using AutoMapper;

namespace MaintainBookApi.Profiles
{
    public class BookProfile:Profile
    {
        public BookProfile() 
        {
            CreateMap<Entities.Book, Models.BookDto>();
            CreateMap<Models.BookForCreationDto, Entities.Book>();
        }
    }
}
