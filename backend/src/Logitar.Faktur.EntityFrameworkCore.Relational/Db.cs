using Logitar.Data;
using Logitar.Faktur.EntityFrameworkCore.Relational.Entities;

namespace Logitar.Faktur.EntityFrameworkCore.Relational;

internal static class Db
{
  public static class Banners
  {
    public static readonly TableId Table = new(nameof(FakturContext.Banners));

    public static readonly ColumnId AggregateId = new(nameof(BannerEntity.AggregateId), Table);
    public static readonly ColumnId DisplayName = new(nameof(BannerEntity.DisplayName), Table);
  }
}
