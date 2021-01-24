using DatascopeTest.DTOs;
using DatascopeTest.Helpers;
using MediatR;

namespace DatascopeTest.Queries
{
    public class GetPagedGamesQuery : IRequest<PagedList<GetGameDto>> 
    {
        private const int MaxPageSize = 100;

        private int _page = 1;
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value < 1 ? _pageSize : value;
        }
    }
}
