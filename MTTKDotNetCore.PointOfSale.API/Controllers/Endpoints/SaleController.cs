using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Features;
using MTTKDotNetCore.PointOfSale.Domain.Models;

namespace MTTKDotNetCore.PointOfSale.API.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : BaseController
    {
        private readonly SaleService _saleService;
        private readonly SaleDetailService _saleDetailService;

        public SaleController(SaleService saleService, SaleDetailService saleDetailService)
        {
            _saleService = saleService;
            _saleDetailService = saleDetailService;
        }

        [HttpGet("get-sale-by-voucher")]
        public async Task<IActionResult> GetSale(string voucherNo)
        {
            var SaleResult = await _saleService.GetSale(voucherNo);
            var saleDetailResult = await _saleDetailService.GetSaleDetails(voucherNo);

            return Execute(SaleResult);
        }

        [HttpPost("sale")]
        public async Task<IActionResult> Sale(SaleRequest saleRequest)
        {
            TblSalePos sale = saleRequest.Sale;
            TblSaleInvoiceDetailPos saleDetail = saleRequest.SaleInvoiceDetail;

            var item = await _saleService.CreateSale(sale);
            var result = await _saleDetailService.CreateSaleInvoiceDetail(saleDetail);

            return result.IsError ? Execute(result) : Execute(item);
        }
    }
}
