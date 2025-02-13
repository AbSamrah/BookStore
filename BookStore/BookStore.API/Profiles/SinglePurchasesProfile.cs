using AutoMapper;

namespace BookStore.API.Profiles
{
    public class SinglePurchasesProfile: Profile
    {
        public SinglePurchasesProfile()
        {
            CreateMap<models.Domain.SinglePurchase, models.DTO.SinglePurchase>().ReverseMap();
        }
    }
}
