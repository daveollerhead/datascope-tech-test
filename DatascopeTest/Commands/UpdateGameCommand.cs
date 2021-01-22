using System;
using MediatR;

namespace DatascopeTest.Commands
{
    public class UpdateGameCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public byte? Rating { get; set; }
    }
}