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
    public class ProductCategoryService
    {
        private readonly AppDbContext _db;

        public ProductCategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<ProductCategoryResponseModel>> CreateProductCategory(TblProductCategoryPos productCategory)
        {
            try
            {
                var productCategoryCode = await _db.TblProductCategoryPos.AsNoTracking()
                    .Where(x => x.DeleteFlag == false)
                    .FirstOrDefaultAsync(x => x.ProductCategoryCode == productCategory.ProductCategoryCode);

                var productCategoryName = await _db.TblProductCategoryPos.AsNoTracking()
                    .Where(x => x.DeleteFlag == false)
                    .FirstOrDefaultAsync(x => x.ProductCategoryName == productCategory.ProductCategoryName);

                if (productCategoryCode != null )
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Product Category Code is alerady exist.");
                }
                if (productCategoryName != null)
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Product Category Name is alerady exist.");
                }
                if (string.IsNullOrEmpty(productCategory.ProductCategoryCode))
                {
                    return Result<ProductCategoryResponseModel>.ValidationError("Product Category Code is alerady exist.");
                }

                await _db.AddAsync(productCategory);
                await _db.SaveChangesAsync();

                var result = new ProductCategoryResponseModel
                {
                    TblProductCategoryPos = productCategory
                };

                return Result<ProductCategoryResponseModel>.Success(result, "Product Category is successfully created.");
            }
            catch (Exception ex)
            {
                return Result<ProductCategoryResponseModel>.SystemError(ex.Message);
            }
        }
    }
}
