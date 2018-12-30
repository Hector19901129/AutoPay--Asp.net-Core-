using AutoPay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPay.DataLayer.EntityConfigurations
{
    internal class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable("Batches");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(40).HasColumnName("FK_AspNetUsers");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.SqlQuery).IsRequired().HasMaxLength(4000);
            builder.Property(x => x.CustomersCount).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedOn).IsRequired(false);
            builder.Property(x => x.Status).IsRequired();

            builder.HasMany(x => x.Customers).WithOne(x => x.Batch).HasForeignKey(x => x.BatchId);
        }
    }
}
