using CrudTask.Domain.Entities;
using CrudTask.Domain.Interfaces;
using CrudTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CrudTask.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Products.AsNoTracking().ToListAsync(cancellationToken);

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default) =>
        await _context.Products.AddAsync(product, cancellationToken);

    public void Update(Product product) =>
        _context.Products.Update(product);

    public void Delete(Product product)
    {
        product.SoftDelete();
        _context.Products.Update(product);
    }
}
