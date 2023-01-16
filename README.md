## Architecture
1) In memory EF DB for purposes of this test - should therefore run without any setup!
2) Minimal API approach for endpoints
3) Use of CQRS with MediatR for infrastructure DI
4) FluentResults to encapsulate results/errors within the core layer and handled in API layer
5) AutoMapper for mapping from core to Dtos

## Testing
1) Use of MSTest for key business logic unit tests regarding bin width calculation
2) Use of NUnit for integration tests on the API - testing the API as an API user