# Tests (45)

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

## EntityFrameworkCore.Relational (14)

- ActorEntity
- ActorService
- AggregateEntity
- BannerEntity
- BannerEventHandler and/or BannerEventConsumers
- BannerQuerier
- BannerRepository
- QueryingExtensions
- SqlHelper
- SqlServerHelper
- StoreEntity
- StoreEventHandler and/or BannerEventConsumers
- StoreQuerier
- StoreRepository

## Infrastructure (3)

- CacheService
- EventBus
- RetryHelper

## Web (2)

- SearchBannersQuery
- SearchStoresQuery
