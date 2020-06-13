using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2.Services.Certificates.Data.Entities;

namespace O2.Services.Certificates.Data.Configurations
{
    public class CertificateEntityConfiguration: IEntityTypeConfiguration<CertificateEntity>
    {
        public void Configure(EntityTypeBuilder<CertificateEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .UseSqlServerIdentityColumn();
        }
    }
}