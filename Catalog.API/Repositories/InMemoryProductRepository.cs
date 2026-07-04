using Catalog.API.Models;

namespace Catalog.API.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly Dictionary<int, Product> _products = new();
    private int _nextId = 1;

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        lock (_products)
        {
            return Task.FromResult<IEnumerable<Product>>(_products.Values.ToList());
        }
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        lock (_products)
        {
            _products.TryGetValue(id, out var product);
            return Task.FromResult(product);
        }
    }

    public Task<Product> CreateAsync(Product product)
    {
        lock (_products)
        {
            var id = _nextId++;
            var newProduct = product with { Id = id };
            _products[id] = newProduct;
            return Task.FromResult(newProduct);
        }
    }

    public Task UpdateAsync(Product product)
    {
        lock (_products)
        {
            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;
            }
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        lock (_products)
        {
            _products.Remove(id);
        }
        return Task.CompletedTask;
    }
}