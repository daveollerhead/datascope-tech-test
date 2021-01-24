using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DatascopeTest.Data.Repositories;
using DatascopeTest.DTOs;
using DatascopeTest.Models;
using FluentValidation;
using MediatR;
using ValidationException = DatascopeTest.Exceptions.ValidationException;

namespace DatascopeTest.Commands
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GetGameDto>
    {
        private readonly IGenericRepository<Game> _repository;
        private readonly IValidator<CreateGameCommand> _validator;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IGenericRepository<Game> repository, IValidator<CreateGameCommand> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GetGameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var game = _mapper.Map<Game>(request);
            _repository.Add(game);
            await _repository.SaveChanges();

            return _mapper.Map<GetGameDto>(game);
        }
    }
}