using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Faktur.Domain.Departments.Events;

public record DepartmentSavedEvent : DomainEvent, INotification
{
  public DepartmentNumber Number { get; }
  public ReadOnlyDepartment Department { get; }

  public DepartmentSavedEvent(ActorId actorId, DepartmentNumber number, ReadOnlyDepartment department)
  {
    ActorId = actorId;
    Number = number;
    Department = department;
  }
}
