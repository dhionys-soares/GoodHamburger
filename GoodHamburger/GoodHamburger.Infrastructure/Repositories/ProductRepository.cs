using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .ToListAsync();
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
}