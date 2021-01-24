using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DatascopeTest.Data.Repositories;
using DatascopeTest.DTOs;
using DatascopeTest.Models;
using MediatR;

namespace DatascopeTest.Queries
{
    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GetGameDto>
    {
        private readonly IGenericRepository<Game> _repository;
        private readonly IMapper _mapper;

        public GetGameQueryHandler(IGenericRepository<Game> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetGameDto> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var game = await _repository.Get(request.Id);
            return _mapper.Map<GetGameDto>(game);
        }
    }
}