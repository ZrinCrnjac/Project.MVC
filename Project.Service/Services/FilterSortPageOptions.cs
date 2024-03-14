using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class FilterSortPageOptions
    {
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
        public string SortOrder { get; set; }
        public int PageSize { get; set; }

        public FilterSortPageOptions()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
    }
}
