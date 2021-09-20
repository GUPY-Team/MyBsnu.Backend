using System;
using System.Collections.Generic;

namespace Shared.DTO
{
    public class PagedList<T>
    {
        public List<T> Items { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int Count { get; }
        public int TotalCount { get; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedList(List<T> items, int currentPage, int pageSize, int totalCount)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageSize = pageSize;
            Count = Items.Count;
            TotalCount = totalCount;
        }
    }
}