using System.Text.Json.Serialization;

namespace Clay.Domain.Entities
{
    public class Lock: BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Label { get; set; }
        public bool IsLocked { get; set; }
        [JsonIgnore]
        public float? ShouldLockAfter { get; set; }
        public DateTime? UnlockedAt { get; set; }
        [JsonIgnore]
        public List<LockRole> LockRoles { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}
