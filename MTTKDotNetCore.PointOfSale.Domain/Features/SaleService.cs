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

        public SaleService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<SaleResponseModel>> CreateSaleAsync(CreateSaleRequest request)
        {
            Result<SaleResponseModel> model = new Result<SaleResponseModel>();
            var totalAmount = 0m;

            string voucherNo = Guid.NewGuid().ToString();

            var sale = new TblSalePos
            {
                VoucherNo = voucherNo,
                SaleDate = DateTime.UtcNow, 
            };

            await _db.TblSalePos.AddAsync(sale);
            await _db.SaveChangesAsync();

            var saleItems = new List<SaleResponseModel.SaleItem>();
            foreach (var item in request.SaleItems)
            {
                var product = await _db.TblProductPos
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(x => x.ProductCode == item.ProductCode && !x.DeleteFlag);

                if (product is null)
                {
                    model = Result<SaleResponseModel>.ValidationError("Product does not exist or is deleted.");
                    goto Result;
                }

                var saleDetail = new TblSaleInvoiceDetailPos
                {
                    VoucherNo = voucherNo,
                    ProductCode = item.ProductCode,
                    Quantity = item.Quantity,
                    Price = product.Price  
                };
                await _db.TblSaleInvoiceDetailPos.AddAsync(saleDetail);

                totalAmount += product.Price * item.Quantity;

                saleItems.Add(new SaleResponseModel.SaleItem
                {
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }
            await _db.SaveChangesAsync();

            sale.TotalAmount = totalAmount;
            _db.TblSalePos.Update(sale);
            await _db.SaveChangesAsync();

            var response = new SaleResponseModel
            {
                VoucherNo = sale.VoucherNo,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                SaleItems = saleItems
            };

            model = Result<SaleResponseModel>.Success(response, "Sale created successfully.");

        Result:
            return model;
        }

        public async Task<Result<SaleResponseModel>> GetSaleAsync(string voucherNo)
        {
            Result<SaleResponseModel> model = new Result<SaleResponseModel>();

            var sale = await _db.TblSalePos.AsNoTracking().FirstOrDefaultAsync(x => x.VoucherNo == voucherNo);
            if (sale is null)
            {
                model = Result<SaleResponseModel>.ValidationError("Sale not found.");
                goto Result;
            }

            var saleDetails = await _db.TblSaleInvoiceDetailPos.AsNoTracking().Where(x => x.VoucherNo == voucherNo).ToListAsync();

            var response = new SaleResponseModel
            {
                VoucherNo = sale.VoucherNo,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                SaleItems = saleDetails.Select(detail => new SaleResponseModel.SaleItem
                {
                    ProductCode = detail.ProductCode,
                    ProductName = _db.TblProductPos.AsNoTracking().FirstOrDefault(x => x.ProductCode == detail.ProductCode)?.ProductName,
                    Quantity = detail.Quantity,
                    Price = detail.Price
                }).ToList()
            };

            model = Result<SaleResponseModel>.Success(response, "Success.");

        Result:
            return model;
        }
    }
}
