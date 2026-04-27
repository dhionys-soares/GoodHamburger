using GoodHamburger.Application.Interfaces.Products;
using GoodHamburger.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Controllers;

[ApiController]
[Route("api/products", Name =  "Products")]
public class ProductsController : ControllerBase
{
    private readonly ICreateProductService _createProductService;
    private readonly IUpdateProductService _updateProductService;
    private readonly IDeleteProductService _deleteProductService;
    private readonly IGetProductByIdService _getProductByIdService;
    private readonly IGetAllProductService _getAllProductService;

    public ProductsController(
        ICreateProductService createProductService,
        IUpdateProductService updateProductService,
        IDeleteProductService deleteProductService,
        IGetProductByIdService getProductByIdService,
        IGetAllProductService getAllProductService)
    {
        _createProductService = createProductService;
        _updateProductService = updateProductService;
        _deleteProductService = deleteProductService;
        _getProductByIdService = getProductByIdService;
        _getAllProductService = getAllProductService;
    }

    [HttpPost(Name =  "CreateProduct")]
    public async Task<IActionResult> Create([FromBody] ProductRequest request)
    {
        var response = await _createProductService.CreateProductAsync(request);

        if (!response.IsSucess)
            return BadRequest(response);

        return Created($"/api/products/{response.Data?.Id}",response);
    }

    [HttpGet(Name = "GetAllProducts")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _getAllProductService.GetAllProductsAsync();

        if (!response.IsSucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new ProductRequest
        {
            Id = id
        };

        var response = await _getProductByIdService.GetProductByIdAsync(request);

        if (!response.IsSucess)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPut("{id:guid}", Name =  "UpdateProduct")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequest request)
    {
        request.Id = id;

        var response = await _updateProductService.UpdateProductAsync(request);

        if (!response.IsSucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpDelete("{id:guid}", Name = "DeleteProduct")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _deleteProductService.DeleteProductAsync(id);

        if (!response.IsSucess)
            return NotFound(response);

        return Ok(response);
    }
}