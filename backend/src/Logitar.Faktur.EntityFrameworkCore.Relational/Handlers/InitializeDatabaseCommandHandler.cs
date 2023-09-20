using Logitar.EventSourcing.EntityFrameworkCore.Relational;
using Logitar.Faktur.Infrastructure.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Logitar.Faktur.EntityFrameworkCore.Relational.Handlers;

internal class InitializeDatabaseCommandHandler : INotificationHandler<InitializeDatabaseCommand>
{
  private readonly IConfiguration _configuration;
  private readonly EventContext _eventContext;
  private readonly FakturContext _fakturContext;

  public InitializeDatabaseCommandHandler(IConfiguration configuration, EventContext eventContext, FakturContext fakturContext)
  {
    _configuration = configuration;
    _eventContext = eventContext;
    _fakturContext = fakturContext;
  }

  public async Task Handle(InitializeDatabaseCommand _, CancellationToken cancellationToken)
  {
    if (_configuration.GetValue<bool>("EnableMigrations"))
    {
      await _eventContext.Database.MigrateAsync(cancellationToken);
      await _fakturContext.Database.MigrateAsync(cancellationToken);
    }
  }
}
