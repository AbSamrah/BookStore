using AutoMapper;

namespace BookStore.API.Profiles
{
    public class BooksProfile: Profile
    {
        public BooksProfile()
        {
            CreateMap<models.Domain.Book, models.DTO.Book>().ReverseMap();
        }
    }
}
