namespace CrudTask.Application.Features.Products.DTOs;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string Category,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
