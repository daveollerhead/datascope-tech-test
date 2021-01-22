using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DatascopeTest.Data;
using DatascopeTest.Data.Repositories;
using DatascopeTest.DTOs;
using DatascopeTest.Helpers;
using MediatR;

namespace DatascopeTest.Queries
{
    public class GetPagedGameQueryHandler : IRequestHandler<GetPagedGamesQuery, PagedList<GetGameDto>>
    {
        private readonly IGamesRepository _repository;
        private readonly IMapper _mapper;

        public GetPagedGameQueryHandler(IGamesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedList<GetGameDto>> Handle(GetPagedGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await _repository.GetPaged(request.Page, request.PageSize);
            var count = await _repository.Count();

            return new PagedList<GetGameDto>(_mapper.Map<IEnumerable<GetGameDto>>(games), count, request.Page, request.PageSize);
        }
    }
}