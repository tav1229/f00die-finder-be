namespace f00die_finder_be.Dtos
{
    public class CustomResponse<T> where T : class
    {
        public T Data { get; set; }
        public List<Error> Error { get; set; }
        public MetaData Meta { get; set; }
    }

    public class MetaData
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}
