using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;
using PS.Web.API.Data;
using System.Threading.Tasks;

namespace PS.Web.API.Extensions
{
    public static class ScraperTextExtensions
    {
        public static async Task UpdateOrCreateAsync(this PolysenseContext dbContext, ScraperText scraperText)
        {
            await dbContext.UpdateOrCreateAsync(scraperText.Category);
            await dbContext.UpdateOrCreateAsync(dbContext.ScraperText, scraperText, s => s.Id == scraperText.Id || s.Text == scraperText.Text);
        }

        public static Task<bool> TryDeleteAsync(this PolysenseContext dbContext, DbSet<ScraperText> dbSet, int id)
        {
            return dbContext.TryDeleteAsync(dbSet, id);
        }

        public static Task<bool> TryDeleteAsync(this PolysenseContext dbContext, ScraperText scraperText)
        {
            return dbContext.TryDeleteAsync(dbContext.ScraperText, scraperText);
        }
    }
}