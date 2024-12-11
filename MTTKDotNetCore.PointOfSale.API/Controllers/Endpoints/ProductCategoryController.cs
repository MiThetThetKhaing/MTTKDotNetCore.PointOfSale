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

        [HttpGet()]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _productCategoryService.GetAllCategories();
            return Execute(result);
        }

        [HttpGet("{categoryCode}")]
        public async Task<IActionResult> GetCategory(string categoryCode)
        {
            var result = await _productCategoryService.GetCategoryByCode(categoryCode);
            return Execute(result);
        }

        [HttpPatch("{categoryCode}")]
        public async Task<IActionResult> UpdateCategory(string categoryCode, string newName)
        {
            var result = await _productCategoryService.ChangeCategoryName(categoryCode, newName);
            return Execute(result);
        }

        [HttpDelete("{categoryCode}")]
        public async Task<IActionResult> DeleteCategory(string categoryCode)
        {
            var result = await _productCategoryService.DeleteCategory(categoryCode);
            return Execute(result);
        }
    }
}
