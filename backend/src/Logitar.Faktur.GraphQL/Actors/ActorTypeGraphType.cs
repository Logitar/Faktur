using GraphQL.Types;
using Logitar.Faktur.Contracts.Actors;

namespace Logitar.Faktur.GraphQL.Actors;

internal class ActorTypeGraphType : EnumerationGraphType<ActorType>
{
  public ActorTypeGraphType()
  {
    Name = nameof(ActorType);
    Description = "Represents the available actor types.";

    Add(ActorType.System, "The actor is the system.");
    Add(ActorType.User, "The actor is an user.");
  }

  private void Add(ActorType value, string description) => Add(value.ToString(), value, description);
}
