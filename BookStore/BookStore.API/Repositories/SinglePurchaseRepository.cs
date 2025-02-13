using BookStore.API.Data;
using BookStore.API.models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class SinglePurchaseRepository : ISinglePurchaseRepository
    {
        private readonly BookStoreDbContext bookStoreDbContext;

        public SinglePurchaseRepository(BookStoreDbContext bookStoreDbContext)
        {
            this.bookStoreDbContext = bookStoreDbContext;
        }
        public async Task<SinglePurchase> AddSinglePurchaseAsync(SinglePurchase singlePurchase)
        {
            singlePurchase.Id = Guid.NewGuid();
            await bookStoreDbContext.AddAsync(singlePurchase);
            await bookStoreDbContext.SaveChangesAsync();
            return singlePurchase;
        }

        public async Task<SinglePurchase> DeleteAsync(Guid id)
        {
            var singlePurchase = await bookStoreDbContext.SinglePurchases.FirstOrDefaultAsync(x => x.Id == id);
            if (singlePurchase == null)
            {
                return null;
            }
            bookStoreDbContext.Remove(singlePurchase);
            await bookStoreDbContext.SaveChangesAsync();
            return singlePurchase;
        }

        public async Task<IEnumerable<SinglePurchase>> GetAllAsync()
        {
            return await bookStoreDbContext.SinglePurchases.ToListAsync();
        }

        public async Task<SinglePurchase> GetByIdAsync(Guid id)
        {
            var singlePurchase = await bookStoreDbContext.SinglePurchases.FirstOrDefaultAsync(x => x.Id == id);
            if (singlePurchase == null)
            {
                return null;
            }
            return singlePurchase;
        }

        public async Task<SinglePurchase> UpdateAsync(Guid id, SinglePurchase singlePurchase)
        {
            var existingSinglePurchase = await bookStoreDbContext.SinglePurchases.FirstOrDefaultAsync(x => x.Id == id);
            if (existingSinglePurchase == null)
            {
                return null;
            }
            existingSinglePurchase.Quantity = singlePurchase.Quantity;
            existingSinglePurchase.BookId = singlePurchase.BookId;
            existingSinglePurchase.PurchaseId = singlePurchase.PurchaseId;
            await bookStoreDbContext.SaveChangesAsync();
            return existingSinglePurchase;
        }
    }
}
