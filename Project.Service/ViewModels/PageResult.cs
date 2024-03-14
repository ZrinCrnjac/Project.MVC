using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.ViewModels
{
    public class PageResult<T> where T : class
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
        public List<T> Items { get; set; }

        public PageResult(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Count = count;
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}
