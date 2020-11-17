using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Shared.Models;

namespace PS.Web.API.Configurations
{
    public class PoliticianConfiguration : IEntityTypeConfiguration<Politician>
    {
        public void Configure(EntityTypeBuilder<Politician> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .HasMaxLength(250);
        }
    }
}