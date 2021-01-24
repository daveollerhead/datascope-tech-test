using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DatascopeTest.Data.Repositories;
using DatascopeTest.Exceptions;
using DatascopeTest.Models;
using FluentValidation;
using MediatR;
using ValidationException = DatascopeTest.Exceptions.ValidationException;

namespace DatascopeTest.Commands
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
    {
        private readonly IGenericRepository<Game> _repository;
        private readonly IValidator<UpdateGameCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(IGenericRepository<Game> repository, IValidator<UpdateGameCommand> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _repository.Get(request.Id);
            if (game == null)
                throw new NoEntityExistsException(nameof(Game), request.Id);

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var updatedDetails = _mapper.Map<Game>(request);
            _mapper.Map(updatedDetails, game);

            await _repository.SaveChanges();

            return Unit.Value;
        }
    }
}