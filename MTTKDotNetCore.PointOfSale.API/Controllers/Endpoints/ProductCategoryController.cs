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
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductCategory(TblProductCategoryPos productCategory)
        {
            try
            {
                var result = await _productCategoryService.CreateProductCategory(productCategory);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await _productCategoryService.GetAllCategories();
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{categoryCode}")]
        public async Task<IActionResult> GetCategory(string categoryCode)
        {
            try
            {
                var result = await _productCategoryService.GetCategoryByCode(categoryCode);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{categoryCode}")]
        public async Task<IActionResult> UpdateCategory(string categoryCode, string newName)
        {
            try
            {
                var result = await _productCategoryService.ChangeCategoryName(categoryCode, newName);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{categoryCode}")]
        public async Task<IActionResult> DeleteCategory(string categoryCode)
        {
            try
            {
                var result = await _productCategoryService.DeleteCategory(categoryCode);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
