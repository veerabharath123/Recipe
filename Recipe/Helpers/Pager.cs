

namespace Recipe.Helpers
{
    public class PagerRequest
    {
        public int currentPage { get; set; } = 1;
        public int pageSize { get; set; } = 12;
        public int maxPages { get; set; } = 10;
        public string? view { get; set; } = "table-view";
                        
    }
    public class PagerResponse<T>
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public IEnumerable<T> Pages { get; set; }
        public string? VIEW { get; set; }
    }
}
