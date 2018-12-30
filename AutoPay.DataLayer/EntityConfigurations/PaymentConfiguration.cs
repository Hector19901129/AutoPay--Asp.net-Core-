using AutoPay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPay.DataLayer.EntityConfigurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.BatchCustomerId).IsRequired().HasColumnName("FK_BatchCustomers");
            builder.Property(x => x.AuthCode).HasMaxLength(50);
            builder.Property(x => x.TransactionId).HasMaxLength(50);
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.IsSuccess).IsRequired();

            builder.HasMany(x => x.Errors).WithOne().HasForeignKey(x => x.PaymentId);
        }
    }
}
