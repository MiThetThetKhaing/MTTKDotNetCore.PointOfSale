using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;

namespace MTTKDotNetCore.PointOfSale.API.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseController
    {
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductCategory(TblProductCategoryPos productCategory)
        {
            var result = await _productCategoryService.CreateProductCategory(productCategory);
            return Execute(result);
        }
    }
}
