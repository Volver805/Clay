namespace Clay.Domain.Entities
{
    public class LockRole: BaseEntity
    {
        public int LockId { get; set; }
        public Lock Lock { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
