using ClientSi.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientSi.INFRASTRUCTURE.Configuration
{
    public class PersonneConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nom)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Prenom)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.IsPro)
                   .HasDefaultValue(false);
        }
    }
}
