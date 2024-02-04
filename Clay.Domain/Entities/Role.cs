namespace Clay.Domain.Entities
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<LockRole> LockRoles { get; set; }
    }
}
