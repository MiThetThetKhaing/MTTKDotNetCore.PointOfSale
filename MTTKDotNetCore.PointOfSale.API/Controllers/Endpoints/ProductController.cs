using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.API.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly AppDbContext _db;
        private readonly IProductService _productService;
        public ProductController(AppDbContext db, IProductService productService)
        {
            _db = db;
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts(string? categoryCode )
        {
            try
            {
                if (categoryCode == null)
                {
                    var result = await _productService.GetAllProducts();
                    return Execute<ProductResponseModel>(result);
                }
                else
                {
                    var result = await _productService.GetAllProductsByCategoryCode(categoryCode);
                    return Execute<ProductResponseModel>(result);
                }
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct(TblProductPos product)
        {
            var result = await _productService.CreateProduct(product);
            return Execute(result);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetProduct(string code)
        {
            var result = await _productService.GetProduct(code);
            return Execute(result);
        }

        [HttpPatch("UpdateProduct/{code}")]
        public async Task<IActionResult> UpdateProduct(string code, TblProductPos updatedProduct)
        {
            var result = await _productService.UpdateProduct(code, updatedProduct);
            return Execute(result);
        }

        [HttpDelete("")] 
        public async Task<IActionResult> DeleteProduct(string code)
        {
            var result = await _productService.DeleteProduct(code);
            return Execute(result);
        }
    }
}
