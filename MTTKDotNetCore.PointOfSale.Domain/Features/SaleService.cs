using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Features
{
    public class SaleService
    {
        private readonly AppDbContext _db;
        private readonly SaleDetailService _saleDetailService;

        public SaleService(AppDbContext db, SaleDetailService saleDetailService)
        {
            _db = db;
            _saleDetailService = saleDetailService;
        }

        public async Task<Result<SaleResponseModel>> CreateSale(TblSalePos sale)
        {
            try
            {
                var Voucher = await _db.TblSalePos.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == sale.VoucherNo);

                if (string.IsNullOrEmpty(sale.VoucherNo))
                {
                    return Result<SaleResponseModel>.ValidationError("You need to put the Voucher No.");
                }

                if (Voucher == null)
                {
                    await _db.TblSalePos.AddAsync(sale);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    _db.Update(sale);
                    await _db.SaveChangesAsync();
                }

                var result = new SaleResponseModel
                {
                    TblSalePos = sale,
                };

                return Result<SaleResponseModel>.Success(result, "Successfully completed.");

            }
            catch (Exception ex)
            {
                return Result<SaleResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
