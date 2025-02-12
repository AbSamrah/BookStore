using BookStore.API.models.Domain;

namespace BookStore.API.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetAll();
    }
}
