using BookStore.API.Data;
using BookStore.API.models.Domain;

namespace BookStore.API.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly BookStoreDbContext bookStoreDbContext;
        public BookRepository(BookStoreDbContext bookStoreDbContext)
        {
            this.bookStoreDbContext = bookStoreDbContext;
        }

        public IEnumerable<Book> GetAll()
        {
            return bookStoreDbContext.Books.ToList();
        }

    }
}
