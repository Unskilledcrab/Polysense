using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;

namespace PS.Web.API.Data
{
    public class PolysenseContext : DbContext
    {
        public PolysenseContext(DbContextOptions<PolysenseContext> options)
            : base(options)
        {
        }

        public DbSet<Bill> Bill { get; set; }
        public DbSet<Politician> Politician { get; set; }
    }
}