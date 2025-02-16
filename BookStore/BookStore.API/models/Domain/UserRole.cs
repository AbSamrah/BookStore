namespace BookStore.API.models.Domain
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
