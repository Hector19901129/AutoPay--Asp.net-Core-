using AutoPay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPay.DataLayer.EntityConfigurations
{
    internal class BatchCustomerDueDetailConfiguration
        : IEntityTypeConfiguration<BatchCustomerDueDetail>
    {
        public void Configure(EntityTypeBuilder<BatchCustomerDueDetail> builder)
        {
            builder.ToTable("BatchCustomerDueDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.BatchCustomerId).IsRequired().HasColumnName("FK_BatchCustomers");
            builder.Property(x => x.RecType).IsRequired().HasMaxLength(128);
            builder.Property(x => x.TransactionDate).HasMaxLength(128);
            builder.Property(x => x.Reference).HasMaxLength(512);
            builder.Property(x => x.Description).HasMaxLength(4000);
            builder.Property(x => x.AmountDue).IsRequired().HasMaxLength(256);
            builder.Property(x => x.YearMonth).HasMaxLength(128);
        }
    }
}
