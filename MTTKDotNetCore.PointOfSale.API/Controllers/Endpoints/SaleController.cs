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

        [HttpPost("sale")]
        public async Task<IActionResult> Sale(SaleRequest saleRequest)
        {
            TblSalePos sale = saleRequest.Sale;
            TblSaleInvoiceDetailPos saleDetail = saleRequest.SaleInvoiceDetail;

            var item = await _saleService.CreateSale(sale);
            var result = await _saleDetailService.CreateSaleInvoiceDetail(saleDetail);

            return Execute(item);
        }
    }
}
