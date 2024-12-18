using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public interface IProductCategoryService
    {
        Task<Result<ProductCategoryResponseModel>> ChangeCategoryName(string categoryCode, string newName);
        Task<Result<ProductCategoryResponseModel>> CreateProductCategory(TblProductCategoryPos productCategory);
        Task<Result<ProductCategoryResponseModel>> DeleteCategory(string categoryCode);
        Task<Result<ProductCategoryResponseModel>> GetAllCategories();
        Task<Result<ProductCategoryResponseModel>> GetCategoryByCode(string categoryCode);
    }
}