using Talabat2.APIs.Dtos;

namespace Talabat2.APIs.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize,int count, IReadOnlyList<T> data)
        {
            PageIndex=pageIndex;
            PageSize=pageSize;
            Data=data;
            this.Count=count;
        }

        //To Change Response Of Date Will Apper To User After Pagination
        //It's Generic Becouse It Will Work With All Data
        public int PageSize { set; get; }
        public int PageIndex { set; get; }
        public int Count { set; get; }
        public IReadOnlyList<T>Data { set; get; }
        
    }
}
