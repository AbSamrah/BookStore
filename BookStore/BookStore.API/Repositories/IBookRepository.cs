using BookStore.API.models.Domain;

namespace BookStore.API.Repositories
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllAsync();
        public Task<Book> GetByIdAsync(Guid id);
        public Task<Book> AddBookAsync(Book book);
    }
}
