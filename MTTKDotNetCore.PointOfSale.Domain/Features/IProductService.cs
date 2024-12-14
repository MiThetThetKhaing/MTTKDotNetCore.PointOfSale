using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public interface IProductService
    {
        Task<Result<ProductResponseModel>> CreateProduct(TblProductPos product);
        Task<Result<ProductResponseModel>> GetProduct(string productCode);
        Task<Result<ProductResponseModel>> UpdateProduct(int productId, TblProductPos updatedProduct); 
        Task<Result<ProductResponseModel>> DeleteProduct(int productId);
    }
}