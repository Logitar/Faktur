﻿using Logitar.EventSourcing;
using Logitar.Faktur.Contracts.Actors;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Configurations;

internal class ActorConfiguration : IEntityTypeConfiguration<ActorEntity>
{
  private const int UrlMaximumLength = 2048;

  public void Configure(EntityTypeBuilder<ActorEntity> builder)
  {
    builder.ToTable(nameof(FakturContext.Actors));
    builder.HasKey(x => x.ActorId);

    builder.HasIndex(x => x.Id).IsUnique();
    builder.HasIndex(x => x.Type);
    builder.HasIndex(x => x.IsDeleted);
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.EmailAddress);

    builder.Property(x => x.Id).HasMaxLength(ActorId.MaximumLength);
    builder.Property(x => x.Type).HasMaxLength(byte.MaxValue).HasConversion(new EnumToStringConverter<ActorType>());
    builder.Property(x => x.DisplayName).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.EmailAddress).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.PictureUrl).HasMaxLength(UrlMaximumLength);
  }
}
