using Logitar.EventSourcing;
using Logitar.Faktur.Domain;
using Logitar.Faktur.Domain.Departments;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Configurations;

internal class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentEntity>
{
  public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
  {
    builder.ToTable(nameof(FakturContext.Departments));
    builder.HasKey(x => x.DepartmentId);

    builder.HasIndex(x => x.Number);
    builder.HasIndex(x => x.DisplayName);
    builder.HasIndex(x => x.CreatedBy);
    builder.HasIndex(x => x.CreatedOn);
    builder.HasIndex(x => x.UpdatedBy);
    builder.HasIndex(x => x.UpdatedOn);
    builder.HasIndex(x => new { x.StoreId, x.Number }).IsUnique();

    builder.HasOne(x => x.Store).WithMany(x => x.Departments).OnDelete(DeleteBehavior.Cascade);

    builder.Property(x => x.Number).HasMaxLength(DepartmentNumber.MaximumLength);
    builder.Property(x => x.DisplayName).HasMaxLength(DisplayName.MaximumLength);
    builder.Property(x => x.CreatedBy).HasMaxLength(ActorId.MaximumLength);
    builder.Property(x => x.UpdatedBy).HasMaxLength(ActorId.MaximumLength);
  }
}
