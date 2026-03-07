using CrudTask.Application.Features.Products.DTOs;
using MediatR;

namespace CrudTask.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string Category
) : IRequest<ProductDto>;
