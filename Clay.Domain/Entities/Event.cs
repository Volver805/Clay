﻿namespace Clay.Domain.Entities
{

    public class Event
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int LockId { get; set; }
        public Lock Lock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
