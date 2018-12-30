using AutoPay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPay.DataLayer.EntityConfigurations
{
    internal class PaymentErrorConfiguration : IEntityTypeConfiguration<PaymentError>
    {
        public void Configure(EntityTypeBuilder<PaymentError> builder)
        {
            builder.ToTable("PaymentErrors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.PaymentId).IsRequired().HasColumnName("FK_Payments");
            builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(512);
        }
    }
}
