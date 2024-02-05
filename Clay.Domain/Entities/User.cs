namespace Clay.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Event> Events { get; set; }
    }
}
