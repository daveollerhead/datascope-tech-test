using System.Threading;
using System.Threading.Tasks;
using DatascopeTest.Data.Repositories;
using DatascopeTest.Exceptions;
using DatascopeTest.Models;
using MediatR;

namespace DatascopeTest.Commands
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IGenericRepository<Game> _repository;

        public DeleteGameCommandHandler(IGenericRepository<Game> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _repository.Get(request.Id);
            if (game == null)
                throw new NoEntityExistsException(nameof(Game), request.Id);

            _repository.Remove(game);
            await _repository.SaveChanges();

            return Unit.Value;
        }
    }
}