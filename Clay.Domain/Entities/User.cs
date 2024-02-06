using System.Text.Json.Serialization;

namespace Clay.Domain.Entities
{
    public class User: BaseEntity
    {
        [JsonIgnore]
        public string Username { get; set; }
        [JsonIgnore]
        public string HashPassword { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}
