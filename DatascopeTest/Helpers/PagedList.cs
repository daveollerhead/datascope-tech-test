using System;
using System.Collections.Generic;

namespace DatascopeTest.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;


        public PagedList(IEnumerable<T> items, int count, int page, int pageSize)
        {
            TotalCount = count;
            Page = page;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }
    }
}