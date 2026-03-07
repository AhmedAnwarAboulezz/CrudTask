using CrudTask.Application.Features.Products.DTOs;
using MediatR;

namespace CrudTask.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
