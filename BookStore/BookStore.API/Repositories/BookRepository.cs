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

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await bookStoreDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            book.Id = Guid.NewGuid();
            await bookStoreDbContext.AddAsync(book);
            bookStoreDbContext.SaveChanges();
            return book;
        }
    }
}
