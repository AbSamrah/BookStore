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
            await bookStoreDbContext.SaveChangesAsync();
            return book;
        }
        public async void DeleteManyAsync(IEnumerable<Guid> ids)
        {
            var p = await bookStoreDbContext.BulkDelete
            return ;
        }

        public async Task<Book> DeleteAsync(Guid id)
        {
            var book = await bookStoreDbContext.Books.FirstOrDefaultAsync(x=>x.Id == id);
            if(book == null)
            {
                return null;
            }
            bookStoreDbContext.Books.Remove(book);
            await bookStoreDbContext.SaveChangesAsync();
            return book;
        }
        public async Task<Book> UpdateAsync(Guid id, Book book)
        {
            var existingBook = await bookStoreDbContext.Books.FirstOrDefaultAsync(x=>x.Id == id);
            if(existingBook == null)
            {
                return null;
            }
            existingBook.Name = book.Name;
            existingBook.PriceInSYR = book.PriceInSYR;
            existingBook.CreatedDate = book.CreatedDate;
            existingBook.AuthorName = book.AuthorName;
            existingBook.Quantity = book.Quantity;

            await bookStoreDbContext.SaveChangesAsync();
            return existingBook;
        }
    }
}
