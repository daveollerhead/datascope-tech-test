using MediatR;

namespace DatascopeTest.Commands
{
    public class DeleteGameCommand : IRequest
    {
        public int Id { get; }

        public DeleteGameCommand(int id)
        {
            Id = id;
        }
    }
}