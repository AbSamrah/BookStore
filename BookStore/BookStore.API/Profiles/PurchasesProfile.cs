using AutoMapper;

namespace BookStore.API.Profiles
{
    public class PurchasesProfile: Profile
    {
        public PurchasesProfile()
        {
            CreateMap<models.Domain.Purchase, models.DTO.Purchase>().ReverseMap();
        }
    }
}
