using BookStore.API.models.Domain;

namespace BookStore.API.Repositories
{
    public interface ISinglePurchaseRepository
    {
        public Task<IEnumerable<SinglePurchase>> GetAllAsync();
        public Task<SinglePurchase> GetByIdAsync(Guid id);
        public Task<SinglePurchase> AddSinglePurchaseAsync(SinglePurchase singlePurchase);
        public Task<SinglePurchase> DeleteAsync(Guid id);
        public Task<SinglePurchase> UpdateAsync(Guid id, SinglePurchase singlePurchase);
    }
}
