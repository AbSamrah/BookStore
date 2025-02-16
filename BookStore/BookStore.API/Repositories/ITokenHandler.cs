using BookStore.API.models.Domain;

namespace BookStore.API.Repositories
{
    public interface ITokenHandler
    {
        public Task<String> CreateTokenAsync(User user);
    }
}
