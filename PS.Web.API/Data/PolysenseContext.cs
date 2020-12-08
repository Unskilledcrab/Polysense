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
        public DbSet<BillStatus> BillStatus { get; set; }
        public DbSet<CongressionalCommitte> CongressionalCommitte { get; set; }
        public DbSet<HouseOfRepresentatives> HouseOfRepresentatives { get; set; }
        public DbSet<Judge> Judge { get; set; }
        public DbSet<Justice> Justice { get; set; }
        public DbSet<Senate> Senate { get; set; }
        public DbSet<SupremeCourt> SupremeCourt { get; set; }
        public DbSet<Politician> Politician { get; set; }
        public DbSet<Congress> Congress { get; set; }

        public DbSet<PS.Shared.Models.TextCategory> TextCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}