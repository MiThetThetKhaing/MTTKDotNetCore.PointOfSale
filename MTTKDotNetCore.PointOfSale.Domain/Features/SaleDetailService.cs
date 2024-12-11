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

        public async Task<Result<SaleResponseModel>> GetSaleDetails(string voucherNo)
        {
            try
            {
                var VoucherNo = await _db.TblSaleInvoiceDetailPos.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == voucherNo);

                if (string.IsNullOrEmpty(VoucherNo.VoucherNo))
                {
                    return Result<SaleResponseModel>.ValidationError("Voucher doesn't exist.");
                }

                var result = new SaleResponseModel()
                {
                    TblSaleInvoiceDetailPos = VoucherNo
                };

                return Result<SaleResponseModel>.Success(result, "Here is your sale details.");
            }
            catch (Exception ex)
            {
                return Result<SaleResponseModel>.SystemError(ex.Message);
            }
        }

        public async Task<Result<SaleResponseModel>> CreateSaleInvoiceDetail(TblSaleInvoiceDetailPos saleInvoice)
        {
            try
            {
                var VoucherNo = await _db.TblSalePos.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleInvoice.VoucherNo);
                var ProductCode = await _db.TblProductPos.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == saleInvoice.ProductCode);
                var VoucherOfSaleDetail = await _db.TblSaleInvoiceDetailPos.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == saleInvoice.VoucherNo);

                if (VoucherNo is null)
                {
                    return Result<SaleResponseModel>.ValidationError("Voucher No doesn't exist in Sale.");
                }
                if (ProductCode is null)
                {
                    return Result<SaleResponseModel>.ValidationError("Product Code doesn't exist. Please Create Product Code first!");
                }
                if (VoucherOfSaleDetail != null)
                {
                    return Result<SaleResponseModel>.ValidationError("Voucher is already exist.");
                }

                //VoucherNo.TotalAmount += ProductCode.Price * saleInvoice.Quantity;
                VoucherNo.TotalAmount = ProductCode.Price * saleInvoice.Quantity;      // this will update total amount of sale
                saleInvoice.Price = ProductCode.Price;  // update price with product price

                _db.TblSalePos.Update(VoucherNo);
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
