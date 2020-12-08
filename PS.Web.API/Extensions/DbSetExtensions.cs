using Microsoft.EntityFrameworkCore;
using PS.Shared.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Web.API.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<PagedResponse<IEnumerable<T>>> GetPageResponse<T>(this IQueryable<T> dbSet, int pageNumber, int pageSize)
        {
            var validFilter = new PaginationFilter(pageNumber, pageSize);
            var pagedData = await dbSet
                .GetPages(pageNumber, pageSize)
                .ToListAsync();
            var totalRecords = await dbSet.CountAsync();
            return new PagedResponse<IEnumerable<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }

        public static IQueryable<T> GetPages<T>(this IQueryable<T> dbSet, int pageNumber, int pageSize)
        {
            var validFilter = new PaginationFilter(pageNumber, pageSize);
            return dbSet
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);
        }

        public static async Task<Response<T>> GetResponseAsync<T>(this DbSet<T> dbSet, int id) where T : class
        {
            var record = await dbSet.FindAsync(id);
            if (record == null) return null;
            return new Response<T>(record);
        }

        public static async Task<Response<IEnumerable<T>>> GetResponseAsync<T>(this DbSet<T> dbSet) where T : class
        {
            var record = await dbSet.ToListAsync();
            if (record == null) return null;
            return new Response<IEnumerable<T>>(record);
        }
    }
}