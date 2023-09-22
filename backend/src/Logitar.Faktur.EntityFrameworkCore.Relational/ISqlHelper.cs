using Logitar.Data;
using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.EntityFrameworkCore.Relational;

public interface ISqlHelper
{
  IQueryBuilder ApplyIdSearch(IQueryBuilder builder, TextSearch search, ColumnId column);
  IQueryBuilder ApplyTextSearch(IQueryBuilder builder, TextSearch search, params ColumnId[] columns);
  IQueryBuilder QueryFrom(TableId table);
}
