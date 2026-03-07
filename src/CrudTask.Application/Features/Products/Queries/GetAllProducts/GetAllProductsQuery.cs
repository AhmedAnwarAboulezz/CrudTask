using CrudTask.Application.Features.Products.DTOs;
using MediatR;

namespace CrudTask.Application.Features.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
