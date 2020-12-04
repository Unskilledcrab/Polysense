namespace PS.Shared.Http
{
    public class PaginationFilter
    {
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            var maxPageSize = 1000; // This is the maximum amount of records an API can return. Subject to change
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 1 || pageSize > maxPageSize ? 1 : pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}