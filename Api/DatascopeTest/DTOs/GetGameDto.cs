using System;

namespace DatascopeTest.DTOs
{
    public class GetGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleasedAt { get; set; }
        public byte Rating { get; set; }
    }
}