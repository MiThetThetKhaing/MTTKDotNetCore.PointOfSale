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
    public class SaleDetailService
    {
        private readonly AppDbContext _db;

        public SaleDetailService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<SaleResponseModel>> CreateSaleInvoiceDetail(TblSaleInvoiceDetailPos saleInvoice)
        {
            try
            {
                var VoucherNo = await _db.TblSalePos.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleInvoice.VoucherNo);
                var Vouchers = await _db.TblSalePos.AsNoTracking().Where(x => x.VoucherNo == saleInvoice.VoucherNo).ToListAsync();
                var ProductCode = await _db.TblProductPos.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == saleInvoice.ProductCode);

                if (VoucherNo is null)
                {
                    return Result<SaleResponseModel>.ValidationError("Voucher No doesn't exist.");
                }
                if (ProductCode is null)
                {
                    return Result<SaleResponseModel>.ValidationError("Product Code doesn't exist. Please Create Product Code first!");
                }
                if (VoucherNo != null)
                {
                    // for all same voucher
                    foreach (var voucher in Vouchers)
                    {
                        VoucherNo.TotalAmount += ProductCode.Price * saleInvoice.Quantity;
                    }

                    _db.TblSalePos.Update(VoucherNo);
                }


                //VoucherNo.TotalAmount = ProductCode.Price * saleInvoice.Quantity;
                saleInvoice.Price = ProductCode.Price;

                await _db.TblSaleInvoiceDetailPos.AddAsync(saleInvoice);
                await _db.SaveChangesAsync();

                var result = new SaleResponseModel()
                {
                    TblSaleInvoiceDetailPos = saleInvoice,
                };

                return Result<SaleResponseModel>.Success(result, "Sale Invoice Detail is successfully completed.");
            }
            catch (Exception ex)
            {
                return Result<SaleResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
