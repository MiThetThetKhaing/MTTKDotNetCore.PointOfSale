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
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("get-sale-by-voucher")]
        public async Task<IActionResult> GetSale(string voucherNo)
        {
            var result = await _saleService.GetSaleAsync(voucherNo);
            return Execute(result);
        }

        //[HttpPost("sale")]
        //public async Task<IActionResult> Sale(SaleRequest saleRequest)
        //{
        //    TblSalePos sale = saleRequest.Sale;
        //    TblSaleInvoiceDetailPos saleDetail = saleRequest.SaleInvoiceDetail;

        //    var item = await _saleService.CreateSale(sale);
        //    var result = await _saleDetailService.CreateSaleInvoiceDetail(saleDetail);

        //    return result.IsError ? Execute(result) : Execute(item);
        //}

        [HttpPost("create")] 
        public async Task<IActionResult> CreateSaleAsync(CreateSaleRequest request) 
        {
            var result = await _saleService.CreateSaleAsync(request); 
            return Execute(result); 
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetSalesByDate(DateTime date)
        {
            var result = await _saleService.GetSaleByDate(date);
            return Execute(result);
        }
    }

}
