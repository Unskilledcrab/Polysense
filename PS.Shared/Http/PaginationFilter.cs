namespace PS.Shared.Http
{
    public class PaginationFilter
    {
        /// <summary>
        /// The default page size of the api if left unspecified
        /// </summary>
        public const int DefaultPageSize = 100;

        /// <summary>
        /// The maximum number of pages an API can return
        /// </summary>
        public const int MaxPageSize = 1000;

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 1 || pageSize > MaxPageSize ? DefaultPageSize : pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}