namespace Catalog.Api.RequestHelpers;

public record PaginationRequest(int PageIndex = 0, int PageSize = 10);