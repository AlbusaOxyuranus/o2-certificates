using Microsoft.EntityFrameworkCore;
using O2.Services.Certificates.Data.Configurations;
using O2.Services.Certificates.Data.Entities;

namespace O2.Services.Certificates.Data
{
    public class CertificateManagementDbContext : DbContext
    {
        public DbSet<CertificateEntity> Certificates { get; set; }

        public CertificateManagementDbContext(DbContextOptions<CertificateManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new CertificateEntityConfiguration());
        }
    }
}