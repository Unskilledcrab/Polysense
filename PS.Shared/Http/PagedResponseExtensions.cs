using System;
using System.Net.Http;

namespace PS.Shared.Http
{
    public static class PagedResponseExtensions
    {
        public static PagedResponse<T> Build<T>(this PagedResponse<T> response, HttpClient client)
        {
            var totalPages = Convert.ToInt32(Math.Ceiling((double)response.TotalRecords / (double)response.PageSize));
            var hasNextPage = response.PageNumber >= 1 && response.PageNumber < totalPages;
            var nextPage = hasNextPage ? new Uri($"{client.BaseAddress}?pageNumber={response.PageNumber + 1}&pageSize={response.PageSize}") : null;
            var hasPreviousPage = response.PageNumber - 1 >= 1 && response.PageNumber <= totalPages;
            var previousPage = hasPreviousPage ? new Uri($"{client.BaseAddress}?pageNumber={response.PageNumber - 1}&pageSize={response.PageSize}") : null;
            var firstPage = new Uri($"{client.BaseAddress}?pageNumber=1&pageSize={response.PageSize}");
            var lastPage = new Uri($"{client.BaseAddress}?pageNumber={totalPages}&pageSize={response.PageSize}");

            response.TotalPages = totalPages;
            response.NextPage = nextPage;
            response.PreviousPage = previousPage;
            response.FirstPage = firstPage;
            response.LastPage = lastPage;
            return response;
        }
    }
}