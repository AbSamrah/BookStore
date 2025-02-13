using BookStore.API.models.Domain;

namespace BookStore.API.Repositories
{
    public interface IPurchaseRepository
    {
        public Task<IEnumerable<Purchase>> GetAllAsync();
        public Task<Purchase> GetByIdAsync(Guid id);
        public Task<Purchase> AddPurchaseAsync(Purchase purchase);
        public Task<Purchase> DeleteAsync(Guid id);
    }
}
