using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Faktur.Domain.Departments.Events;

public record DepartmentRemovedEvent : DomainEvent, INotification
{
  public DepartmentNumber Number { get; }

  public DepartmentRemovedEvent(ActorId actorId, DepartmentNumber number)
  {
    ActorId = actorId;
    Number = number;
  }
}
