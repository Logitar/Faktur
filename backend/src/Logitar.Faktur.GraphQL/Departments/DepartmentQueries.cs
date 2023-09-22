using GraphQL;
using GraphQL.Types;
using Logitar.Faktur.Contracts.Departments;

namespace Logitar.Faktur.GraphQL.Departments;

internal static class DepartmentQueries
{
  public static void Register(RootQuery root)
  {
    root.Field<DepartmentGraphType>("department") // TODO(fpion): Authorization
      .Description("Retrieves a single department.")
      .Arguments(
        new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "storeId", Description = "The unique identifier of the store." },
        new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "departmentNumber", Description = "The number of the department." }
      )
      .ResolveAsync(async context => await context.GetRequiredService<IDepartmentService, object?>().ReadAsync(
        context.GetArgument<string>("storeId"),
        context.GetArgument<string>("departmentNumber"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<DepartmentSearchResultsGraphType>>("departments") // TODO(fpion): Authorization
      .Description("Searches a list of departments.")
      .Arguments(
        new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "storeId", Description = "The unique identifier of the store." },
        new QueryArgument<NonNullGraphType<SearchDepartmentsPayloadGraphType>>() { Name = "payload", Description = "The parameters to apply to the search." }
      )
      .ResolveAsync(async context => await context.GetRequiredService<IDepartmentService, object?>().SearchAsync(
        context.GetArgument<string>("storeId"),
        context.GetArgument<SearchDepartmentsPayload>("payload"),
        context.CancellationToken
      ));
  }
}
