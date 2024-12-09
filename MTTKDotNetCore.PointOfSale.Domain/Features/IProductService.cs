using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public interface IProductService
    {
        Task<Result<ProductResponseModel>> CreateProduct(TblProductPos product);
    }
}