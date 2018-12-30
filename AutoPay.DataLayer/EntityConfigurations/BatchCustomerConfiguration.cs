using AutoPay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPay.DataLayer.EntityConfigurations
{
    internal class BatchCustomerConfiguration : IEntityTypeConfiguration<BatchCustomer>
    {
        public void Configure(EntityTypeBuilder<BatchCustomer> builder)
        {
            builder.ToTable("BatchCustomers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.BatchId).HasColumnName("FK_Batches");
            builder.Property(x => x.CustomerId).HasMaxLength(256);
            builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(512);
            builder.Property(x => x.AmountDue).IsRequired().HasMaxLength(256);
            builder.Property(x => x.IsExistsInLocalDb).IsRequired().HasMaxLength(256);
            builder.Property(x => x.PaymentStatus).IsRequired().HasMaxLength(256);

            builder.HasMany(x => x.Payments).WithOne().HasForeignKey(x => x.BatchCustomerId);
            builder.HasMany(x => x.DueDetails).WithOne().HasForeignKey(x => x.BatchCustomerId);
        }
    }
}
