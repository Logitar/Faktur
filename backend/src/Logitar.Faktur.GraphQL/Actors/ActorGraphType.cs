﻿using GraphQL.Types;
using Logitar.Faktur.Contracts.Actors;

namespace Logitar.Faktur.GraphQL.Actors;

internal class ActorGraphType : ObjectGraphType<Actor>
{
  public ActorGraphType()
  {
    Name = nameof(Actor);
    Description = "Represents an actor in the system.";

    Field(x => x.Id)
      .Description("The unique identifier of the actor.");
    Field(x => x.Type, type: typeof(NonNullGraphType<ActorTypeGraphType>))
      .Description("The type of the actor.");
    Field(x => x.IsDeleted)
      .Description("A value indicating whether or not the actor is deleted.");

    Field(x => x.DisplayName)
      .Description("The display name of the actor.");
    Field(x => x.EmailAddress, nullable: true)
      .Description("The email address of the actor.");
    Field(x => x.PictureUrl, nullable: true)
      .Description("The URL to the picture of the actor.");
  }
}
