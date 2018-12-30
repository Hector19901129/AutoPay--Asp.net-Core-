using AutoPay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPay.DataLayer.EntityConfigurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Address).HasMaxLength(512);
            builder.Property(x => x.City).HasMaxLength(256);
            builder.Property(x => x.State).HasMaxLength(256);
            builder.Property(x => x.CountryId).HasColumnName("FK_Countries").HasMaxLength(32);
            builder.Property(x => x.ZipCode).HasMaxLength(128);
            builder.Property(x => x.CardType).IsRequired().HasMaxLength(32);
            builder.Property(x => x.CardNumber).IsRequired().HasMaxLength(128);
            builder.Property(x => x.ExpiryMonth).IsRequired().HasMaxLength(32);
            builder.Property(x => x.ExpiryYear).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Ccv).IsRequired().HasMaxLength(32);
            builder.Property(x => x.CreatedOn).IsRequired().HasMaxLength(128);
            builder.Property(x => x.CreatedBy).HasColumnName("FK_AspNetUsers").IsRequired().HasMaxLength(128);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(32);
        }
    }
}
