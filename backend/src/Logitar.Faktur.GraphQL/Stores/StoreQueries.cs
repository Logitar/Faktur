using GraphQL;
using GraphQL.Types;
using Logitar.Faktur.Contracts.Stores;

namespace Logitar.Faktur.GraphQL.Stores;

internal static class StoreQueries
{
  public static void Register(RootQuery root)
  {
    root.Field<StoreGraphType>("store") // TODO(fpion): Authorization
      .Description("Retrieves a single store.")
      .Arguments(
        new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "id", Description = "The unique identifier of the store." }
      )
      .ResolveAsync(async context => await context.GetRequiredService<IStoreService, object?>().ReadAsync(
        context.GetArgument<string>("id"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<StoreSearchResultsGraphType>>("stores") // TODO(fpion): Authorization
      .Description("Searches a list of stores.")
      .Arguments(
        new QueryArgument<NonNullGraphType<SearchStoresPayloadGraphType>>() { Name = "payload", Description = "The parameters to apply to the search." }
      )
      .ResolveAsync(async context => await context.GetRequiredService<IStoreService, object?>().SearchAsync(
        context.GetArgument<SearchStoresPayload>("payload"),
        context.CancellationToken
      ));
  }
}
