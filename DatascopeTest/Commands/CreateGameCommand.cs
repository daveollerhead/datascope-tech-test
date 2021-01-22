using System;
using DatascopeTest.DTOs;
using MediatR;

namespace DatascopeTest.Commands
{
    public class CreateGameCommand : IRequest<GetGameDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public byte? Rating { get; set; }
    }
}
