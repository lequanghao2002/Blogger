namespace Blogger_Web.Infrastructure.Core
{
    public class PaginationSet2<T>
    {
        public int Page { get; set; }
        public int Count
        {
            get
            {
                return (List != null) ? List.Count() : 0;
            }
        }

        // Lưu tổng số trang
        public int PagesCount { get; set; }

        // Lưu tổng số bản ghi
        public int TotalCount { get; set; }

        // Lưu tổng số trang hiển thị
        public int MaxPage { get; set; }  

        public IEnumerable<T> List { get; set; }
    }
}
