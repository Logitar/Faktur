using GraphQL;
using GraphQL.Types;
using Logitar.Faktur.Contracts.Banners;

namespace Logitar.Faktur.GraphQL.Banners;

internal static class BannerQueries
{
  public static void Register(RootQuery root)
  {
    root.Field<BannerGraphType>("banner") // TODO(fpion): Authorization
      .Description("Retrieves a single banner.")
      .Arguments(
        new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "id", Description = "The unique identifier of the banner." }
      )
      .ResolveAsync(async context => await context.GetRequiredService<IBannerService, object?>().ReadAsync(
        context.GetArgument<string>("id"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<BannerSearchResultsGraphType>>("banners") // TODO(fpion): Authorization
      .Description("Searches a list of banners.")
      .Arguments(
        new QueryArgument<NonNullGraphType<SearchBannersPayloadGraphType>>() { Name = "payload", Description = "The parameters to apply to the search." }
      )
      .ResolveAsync(async context => await context.GetRequiredService<IBannerService, object?>().SearchAsync(
        context.GetArgument<SearchBannersPayload>("payload"),
        context.CancellationToken
      ));
  }
}
