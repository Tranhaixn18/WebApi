using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webMobile.ModelView
{
    public class PageResult<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
        public List<T> Items { set; get; }
    }
}
