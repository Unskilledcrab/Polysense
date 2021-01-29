using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;
using PS.Web.API.Data;
using System.Threading.Tasks;

namespace PS.Web.API.Extensions
{
    public static class ScraperTextExtensions
    {
        public static async Task UpdateOrCreate(this PolysenseContext dbContext, ScraperText scraperText)
        {
            await dbContext.UpdateOrCreate(scraperText.Category);
            await dbContext.UpdateOrCreate(dbContext.ScraperText, scraperText, s => s.Id == scraperText.Id || s.Text == scraperText.Text);
        }

        public static Task<bool> TryDelete(this PolysenseContext dbContext, DbSet<ScraperText> dbSet, int id)
        {
            return dbContext.TryDelete(dbSet, id);
        }

        public static Task<bool> TryDelete(this PolysenseContext dbContext, ScraperText scraperText)
        {
            return dbContext.TryDelete(dbContext.ScraperText, scraperText);
        }
    }
}