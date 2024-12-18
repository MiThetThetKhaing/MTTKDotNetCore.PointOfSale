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

        [HttpGet("{voucherNo}")]
        public async Task<IActionResult> GetSale(string voucherNo)
        {
            try
            {
                var result = await _saleService.GetSaleAsync(voucherNo);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/create")]
        public async Task<IActionResult> CreateSaleAsync(CreateSaleRequest request)
        {
            try
            {
                var result = await _saleService.CreateSaleAsync(request);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{date}")]
        public async Task<IActionResult> GetSalesByDate(DateTime date)
        {
            try
            {
                var result = await _saleService.GetSaleByDate(date);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/month/{month}/{year}")]
        public async Task<IActionResult> GetSalesByMonth(int month, int year)
        {
            try
            {
                var result = await _saleService.GetSaleByMonth(month, year);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
