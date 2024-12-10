using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(TblProductPos product)
        {
            var result = await _productService.CreateProduct(product);
            return Execute(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productService.GetProduct(id);
            return Ok(result);
        }

        [HttpPatch("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, TblProductPos updatedProduct)
        {
            var result = await _productService.UpdateProduct(id, updatedProduct);
            return Ok(result);
        }

        [HttpDelete("Delete")] 
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }
    }
}
