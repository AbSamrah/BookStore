namespace BookStore.API.Repositories
{
    public interface IUserRepository
    {
        Task<models.Domain.User> AuthenticateAsync(String userName, String password);
    }
}
