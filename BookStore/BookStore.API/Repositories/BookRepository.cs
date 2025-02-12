using BookStore.API.Data;
using BookStore.API.models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly BookStoreDbContext bookStoreDbContext;
        public BookRepository(BookStoreDbContext bookStoreDbContext)
        {
            this.bookStoreDbContext = bookStoreDbContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await bookStoreDbContext.Books.ToListAsync();
        }

    }
}
