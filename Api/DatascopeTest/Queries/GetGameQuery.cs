using DatascopeTest.DTOs;
using MediatR;

namespace DatascopeTest.Queries
{
    public class GetGameQuery : IRequest<GetGameDto>
    {
        public int Id { get; }

        public GetGameQuery(int id)
        {
            Id = id;
        }
    }
}