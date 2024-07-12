using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Specifications
{
    public class ProductSpecParams
    {
        public string? Sort { set; get; }
        public int? BrandId { set; get; }
        public int? TypeId { set; get;}
        private const int MaxPageSize = 10;
        private int pageSize = 5;
        public int PageSize 
        { 
            set { pageSize=value>MaxPageSize ? MaxPageSize : value; }
            get { return pageSize; }
        }
        public int PageIndex { set; get; } = 1;
        private string search { set; get; }
        public string? Search {
            set { search=value.ToLower(); }
            get { return search; }
        }
    }
}
