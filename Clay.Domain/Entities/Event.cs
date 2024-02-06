 using System.Text.Json.Serialization;

namespace Clay.Domain.Entities
{

    public class Event: BaseEntity
    {
        public string Type { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public int LockId { get; set; }
        public Lock Lock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
