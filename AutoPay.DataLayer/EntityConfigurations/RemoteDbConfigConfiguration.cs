using AutoPay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPay.DataLayer.EntityConfigurations
{
    internal class RemoteDbConfigConfiguration : IEntityTypeConfiguration<RemoteDbConfig>
    {
        public void Configure(EntityTypeBuilder<RemoteDbConfig> builder)
        {
            builder.ToTable("RemoteDbConfigs");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId).HasMaxLength(40).ValueGeneratedNever();
            builder.Property(x => x.Server).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Database).IsRequired().HasMaxLength(256);
            builder.Property(x => x.UpdateDueDetailSp).IsRequired().HasMaxLength(512);
            builder.Property(x => x.GetDueDetailQuery).IsRequired().HasMaxLength(4000);
        }
    }
}
