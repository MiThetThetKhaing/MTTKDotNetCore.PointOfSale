using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public interface IProductService
    {
        Task<Result<ProductResponseModel>> CreateProduct(TblProductPos product);
        Task<Result<TblProductPos>> GetProduct(string productCode);
        Task<Result<ProductResponseModel>> UpdateProduct(string productCode, TblProductPos updatedProduct); 
        Task<Result<ProductResponseModel>> DeleteProduct(string productCode);
    }
}