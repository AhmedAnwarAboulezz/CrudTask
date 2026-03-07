using CrudTask.Domain.Common;
using CrudTask.Domain.Exceptions;
using CrudTask.Domain.ValueObjects;

namespace CrudTask.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } = null!;
    public string Category { get; private set; } = string.Empty;

    private Product() { }

    public static Product Create(string name, string description, decimal price, string currency, string category)
    {
        ValidateName(name);
        ValidateCategory(category);

        return new Product
        {
            Name = name.Trim(),
            Description = description.Trim(),
            Price = Money.Create(price, currency),
            Category = category.Trim()
        };
    }

    public void Update(string name, string description, decimal price, string currency, string category)
    {
        ValidateName(name);
        ValidateCategory(category);

        Name = name.Trim();
        Description = description.Trim();
        Price = Money.Create(price, currency);
        Category = category.Trim();

        SetUpdatedAt();
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty.");

        if (name.Length > 200)
            throw new DomainException("Product name cannot exceed 200 characters.");
    }

    private static void ValidateCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new DomainException("Product category cannot be empty.");
    }
}
