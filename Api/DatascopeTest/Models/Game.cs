using System;

namespace DatascopeTest.Models
{
    public class Game : Entity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime ReleasedAt { get; private set; }
        public DateTime CreatedAt { get; }
        public byte Rating { get; private set; }

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
