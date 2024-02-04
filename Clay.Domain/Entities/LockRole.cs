namespace Clay.Domain.Entities
{
    public class LockRole
    {
        public int ID {  get; set; }
        public int LockId { get; set; }
        public Lock Lock { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
