using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Stores;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Configurations;

internal class StoreConfiguration : AggregateConfiguration<StoreEntity>, IEntityTypeConfiguration<StoreEntity>
{
  public override void Configure(EntityTypeBuilder<StoreEntity> builder)
  {
    base.Configure(builder);

    builder.ToTable(nameof(FakturContext.Stores));
    builder.HasKey(x => x.StoreId);

    builder.HasIndex(x => x.Number);
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.AddressFormatted);
    builder.HasIndex(x => x.PhoneE164Formatted);

    builder.HasOne(x => x.Banner).WithMany(x => x.Stores).OnDelete(DeleteBehavior.Restrict);

    builder.Property(x => x.Number).HasMaxLength(StoreNumber.MaximumLength);
    builder.Property(x => x.DisplayName).HasMaxLength(DisplayName.MaximumLength);
    builder.Property(x => x.AddressStreet).HasMaxLength(ReadOnlyAddress.StreetMaximumLength);
    builder.Property(x => x.AddressLocality).HasMaxLength(ReadOnlyAddress.LocalityMaximumLength);
    builder.Property(x => x.AddressRegion).HasMaxLength(ReadOnlyAddress.RegionMaximumLength);
    builder.Property(x => x.AddressPostalCode).HasMaxLength(ReadOnlyAddress.PostalCodeMaximumLength);
    builder.Property(x => x.AddressCountry).HasMaxLength(ReadOnlyAddress.CountryMaximumLength);
    builder.Property(x => x.AddressFormatted).HasMaxLength(1000);
    builder.Property(x => x.PhoneCountryCode).HasMaxLength(ReadOnlyPhone.CountryCodeMaximumLength);
    builder.Property(x => x.PhoneNumber).HasMaxLength(ReadOnlyPhone.NumberMaximumLength);
    builder.Property(x => x.PhoneExtension).HasMaxLength(ReadOnlyPhone.ExtensionMaximumLength);
    builder.Property(x => x.PhoneE164Formatted).HasMaxLength(40);
  }
}
