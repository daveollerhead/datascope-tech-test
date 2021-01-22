using System;

namespace DatascopeTest.Models
{
    public class Game : Entity
    {
        public int Id { get; private set; }
        public string Name { get; }
        public string Description { get; }
        public DateTime ReleasedAt { get; }
        public DateTime CreatedAt { get; }
        public byte Rating { get; }

        public Game(string name, string description, DateTime releasedAt, byte rating)
        {
            Name = name;
            Description = description;
            ReleasedAt = releasedAt;
            CreatedAt = DateTime.UtcNow;
            Rating = rating;
        }
    }
}
