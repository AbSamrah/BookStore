using BookStore.API.Data;
using BookStore.API.models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreDbContext bookStoreDbContext;

        public UserRepository(BookStoreDbContext bookStoreDbContext)
        {
            this.bookStoreDbContext = bookStoreDbContext;
        }

        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            var user = await bookStoreDbContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower()&&x.Password==password);
            if (user == null)
            {
                return null;
            }

            var userRoles = await bookStoreDbContext.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles)
                {
                    
                    var role = await bookStoreDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if(role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }

            }
            user.Password = null;
            return user;
        }
    }
}
