using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public interface IProductService
    {
        Task<Result<ProductResponseModel>> GetAllProducts();
        Task<Result<ProductResponseModel>> GetAllProductsByCategoryCode(String categoryCode);
        Task<Result<ProductResponseModel>> CreateProduct(TblProductPos product);
        Task<Result<ProductResponseModel>> GetProduct(string productCode);
        Task<Result<ProductResponseModel>> UpdateProduct(string productCode, TblProductPos updatedProduct); 
        Task<Result<ProductResponseModel>> DeleteProduct(string productCode);
    }
}