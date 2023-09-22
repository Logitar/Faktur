using GraphQL.Types;
using Logitar.Faktur.Contracts.Departments;
using Logitar.Faktur.GraphQL.Actors;
using Logitar.Faktur.GraphQL.Stores;

namespace Logitar.Faktur.GraphQL.Departments;

internal class DepartmentGraphType : ObjectGraphType<Department>
{
  public DepartmentGraphType()
  {
    Name = nameof(Department);
    Description = "TODO";

    Field(x => x.Number)
      .Description("The number of the department.");
    Field(x => x.DisplayName)
      .Description("The display name of the department.");
    Field(x => x.Description, nullable: true)
      .Description("The description of the department.");

    Field(x => x.Version)
      .Description("The version of the department.");

    Field(x => x.CreatedBy, type: typeof(NonNullGraphType<ActorGraphType>))
      .Description("The actor who created the department.");
    Field(x => x.CreatedOn)
      .Description("The date and time when the redepartmentsource was created.");

    Field(x => x.UpdatedBy, type: typeof(NonNullGraphType<ActorGraphType>))
      .Description("The actor who updated the department lastly.");
    Field(x => x.UpdatedOn)
      .Description("The date and time when the department was updated lastly.");

    Field(x => x.Store, type: typeof(StoreGraphType))
      .Description("TODO");
  }
}
