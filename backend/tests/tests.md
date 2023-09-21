# Tests (40)

## Application (16)

### Miscellaneous (4)

- AggregateIdExtensions
- ApplicationContextExtensions
- ExceptionExtensions
- ExceptionMessageBuilder

### Command/Query Handlers (12)

- CreateBannerCommandHandler
- CreateStoreCommandHandler
- DeleteBannerCommandHandler
- DeleteStoreCommandHandler
- ReadBannerQueryHandler
- ReadStoreQueryHandler
- ReplaceBannerCommandHandler
- ReplaceStoreCommandHandler
- SearchBannersQueryHandler
- SearchStoresQueryHandler
- UpdateBannerCommandHandler
- UpdateStoreCommandHandler

## Domain (10)

- BannerAggregate
- BannerId
- Description
- DisplayName
- FluentValidationExtensions
- ReadOnlyAddress
- ReadOnlyPhone
- StoreAggregate
- StoreId
- StoreNumber

## EntityFrameworkCore.Relational (10)

- ActorEntity
- ActorService
- AggregateEntity
- BannerEntity
- BannerEventHandler and/or BannerEventConsumers
- BannerQuerier
- QueryingExtensions
- BannerRepository
- SqlHelper
- SqlServerHelper

## Infrastructure (3)

- CacheService
- EventBus
- RetryHelper

## Web (1)

- SearchBannersQuery
