using Logitar.Data;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;

namespace Logitar.Faktur.EntityFrameworkCore.Relational;

internal static class Db
{
  public static class Banners
  {
    public static readonly TableId Table = new(nameof(FakturContext.Banners));

    public static readonly ColumnId AggregateId = new(nameof(BannerEntity.AggregateId), Table);
    public static readonly ColumnId BannerId = new(nameof(BannerEntity.BannerId), Table);
    public static readonly ColumnId DisplayName = new(nameof(BannerEntity.DisplayName), Table);
  }

  public static class Stores
  {
    public static readonly TableId Table = new(nameof(FakturContext.Stores));

    public static readonly ColumnId AddressFormatted = new(nameof(StoreEntity.AddressFormatted), Table);
    public static readonly ColumnId AggregateId = new(nameof(StoreEntity.AggregateId), Table);
    public static readonly ColumnId BannerId = new(nameof(StoreEntity.BannerId), Table);
    public static readonly ColumnId DisplayName = new(nameof(StoreEntity.DisplayName), Table);
    public static readonly ColumnId Number = new(nameof(StoreEntity.Number), Table);
    public static readonly ColumnId PhoneE164Formatted = new(nameof(StoreEntity.PhoneE164Formatted), Table);
  }
}
