using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services;

public class DeleteProductService : IDeleteProductService
{
    private readonly IProductRepository _productRepository;

    public DeleteProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Response<Product>> DeleteProductAsync(Guid id)
    {
        if (id == null)
            return Response<Product>.Fail("Invalid product id", "400");

        var product = await _productRepository.GetProductByIdAsync(id);

        if (product is null)
            return Response<Product>.Fail("Product not found", "404");

        await _productRepository.DeleteProductAsync(product.Id);
        return Response<Product>.Ok(product, "Product successfully deleted");
    }
}