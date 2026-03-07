using CrudTask.Application.Features.Products.DTOs;
using CrudTask.Domain.Entities;

namespace CrudTask.Application.Features.Products;

internal static class ProductMappingExtensions
{
    internal static ProductDto ToDto(this Product product) =>
        new(
            product.Id,
            product.Name,
            product.Description,
            product.Price.Amount,
            product.Price.Currency,
            product.Category,
            product.CreatedAt,
            product.UpdatedAt
        );
}
