using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTSTools.BLL.Common
{
    public class PagedResultDTO<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int TotalCount { get; set; }
        public string SortPropertyName { get; set; }
        public bool? SortDescending { get; set; }
        public IList dxFilters { get; set; }
        public T Filter { get; set; }
        public List<T> DataList { get; set; }
        public PagedResultDTO()
        {
            DataList = new List<T>();
        }
    }
}
