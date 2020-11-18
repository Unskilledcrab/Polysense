using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Shared.Models;

namespace PS.Web.API.Configurations
{
    public class CongressConfiguration : IEntityTypeConfiguration<Congress>
    {
        public void Configure(EntityTypeBuilder<Congress> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .HasMaxLength(150);
        }
    }
}