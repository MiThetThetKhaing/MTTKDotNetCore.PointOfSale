using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public interface ISaleService
    {
        Task<Result<SaleResponseModel>> CreateSaleAsync(CreateSaleRequest request);
        Task<Result<SaleResponseModel>> GetSaleAsync(string voucherNo);
        Task<Result<List<SaleResponseModel>>> GetSaleByDate(DateTime date);
        Task<Result<List<SaleResponseModel>>> GetSaleByMonth(int month, int year);
    }
}