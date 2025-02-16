using BookStore.API.models.Domain;

namespace BookStore.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> Users;

        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            var user = Users.Find(x => x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase)&&x.Password==password);
            return user;
        }
    }
}
