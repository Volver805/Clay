namespace Clay.Domain.Entities
{
    public class Lock: BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Label { get; set; }
        public bool IsLocked { get; set; }
        public float? ShouldLockAfter { get; set; }
        public DateTime? UnlockedAt { get; set; }
        public List<LockRole> LockRoles { get; set; }
        public List<Event> Events { get; set; }
    }
}
