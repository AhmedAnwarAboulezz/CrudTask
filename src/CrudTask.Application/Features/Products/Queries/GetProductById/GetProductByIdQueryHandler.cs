using CrudTask.Application.Features.Products.DTOs;
using CrudTask.Domain.Entities;
using CrudTask.Domain.Exceptions;
using CrudTask.Domain.Interfaces;
using MediatR;

namespace CrudTask.Application.Features.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Product), request.Id);

        return product.ToDto();
    }
}
