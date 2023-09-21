using GraphQL.Types;
using Logitar.Faktur.Contracts;
using Logitar.Faktur.GraphQL.Actors;

namespace Logitar.Faktur.GraphQL;

internal abstract class AggregateGraphType<T> : ObjectGraphType<T> where T : Aggregate
{
  protected AggregateGraphType(string? description = null)
  {
    Name = typeof(T).Name;
    Description = description;

    Field(x => x.Id)
      .Description("The unique identifier of the resource.");
    Field(x => x.Version)
      .Description("The version of the resource.");

    Field(x => x.CreatedBy, type: typeof(NonNullGraphType<ActorGraphType>))
      .Description("The actor who created the resource.");
    Field(x => x.CreatedOn)
      .Description("The date and time when the resource was created.");

    Field(x => x.UpdatedBy, type: typeof(NonNullGraphType<ActorGraphType>))
      .Description("The actor who updated the resource lastly.");
    Field(x => x.UpdatedOn)
      .Description("The date and time when the resource was updated lastly.");
  }
}
