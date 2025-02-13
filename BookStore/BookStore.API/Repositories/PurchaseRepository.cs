using BookStore.API.models.Domain;
using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;


namespace BookStore.API.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly BookStoreDbContext bookStoreDbContext;

        public PurchaseRepository(BookStoreDbContext bookStoreDbContext)
        {
            this.bookStoreDbContext = bookStoreDbContext;
        }
        public async Task<Purchase> AddPurchaseAsync(Purchase purchase)
        {
            purchase.Id = Guid.NewGuid();
            await bookStoreDbContext.AddAsync(purchase);
            await bookStoreDbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<Purchase> DeleteAsync(Guid id)
        {
            var purchase = await bookStoreDbContext.Purchases.FirstOrDefaultAsync(x => x.Id == id);
            if(purchase == null)
            {
                return null;
            }
            bookStoreDbContext.Remove(purchase);
            await bookStoreDbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await bookStoreDbContext.Purchases.ToListAsync();
        }

        public async Task<Purchase> GetByIdAsync(Guid id)
        {
            var purchase = await bookStoreDbContext.Purchases.FirstOrDefaultAsync(x => x.Id == id);
            if (purchase == null)
            {
                return null;
            }
            return purchase;
        }

        public async Task<Purchase> UpdateAsync(Guid id, Purchase purchase)
        {
            var existingPurchase = await bookStoreDbContext.Purchases.FirstOrDefaultAsync(x => x.Id == id);
            if(existingPurchase == null)
            {
                return null;
            }
            existingPurchase.SinglePurchases = purchase.SinglePurchases;
            existingPurchase.PurchaseTime = purchase.PurchaseTime;
            return existingPurchase;
        }
    }
}
