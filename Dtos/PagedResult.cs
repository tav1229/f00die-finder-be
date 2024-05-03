namespace f00die_finder_be.Dtos
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
    }
}
