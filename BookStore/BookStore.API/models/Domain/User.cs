using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.API.models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public String EmailAddress { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [NotMapped]
        public List<String> Roles { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
