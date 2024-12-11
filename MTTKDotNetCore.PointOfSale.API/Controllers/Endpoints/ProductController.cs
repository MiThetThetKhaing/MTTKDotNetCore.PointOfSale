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
    }
}
