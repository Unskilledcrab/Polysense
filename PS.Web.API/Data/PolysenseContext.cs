using Microsoft.EntityFrameworkCore;
using PS.Shared.Models;
using System.Reflection;

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

        public DbSet<Congress> Congress { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}