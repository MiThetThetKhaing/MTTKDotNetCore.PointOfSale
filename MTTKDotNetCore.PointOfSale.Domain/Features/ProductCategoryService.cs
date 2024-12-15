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
                    return Result<ProductCategoryResponseModel>.ValidationError("Product Category Code can't be empty or null.");
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

        public async Task<Result<ProductCategoryResponseModel>> GetAllCategories()
        {
            Result<ProductCategoryResponseModel> response = new Result<ProductCategoryResponseModel>();

            var lst = await _db.TblProductCategoryPos.AsNoTracking().Where(c => c.DeleteFlag == false).ToListAsync();

            ProductCategoryResponseModel model = new ProductCategoryResponseModel { 
                TblProductCategoryPosList = lst
            };
            if(lst.Count > 0)
            {
                response = Result<ProductCategoryResponseModel>.Success(model, "There is no Category. Try creating one, frist!");
                goto Result;
            }

            response = Result<ProductCategoryResponseModel>.Success(model, "Here are the categories: ");

        Result:
            return response;
        }

        public async Task<Result<ProductCategoryResponseModel>> GetCategoryByCode(string categoryCode)
        {
            Result<ProductCategoryResponseModel> response = new Result<ProductCategoryResponseModel>();

            var category = await _db.TblProductCategoryPos.AsNoTracking().Where(c => c.DeleteFlag == false && c.ProductCategoryCode == categoryCode ).FirstOrDefaultAsync();

            
            if (category is null)
            {
                response = Result<ProductCategoryResponseModel>.NotFound($"Can't find the Category with code {categoryCode}.");
                goto Result;
            }

            ProductCategoryResponseModel model = new ProductCategoryResponseModel
            {
                TblProductCategoryPos = category
            };
            response = Result<ProductCategoryResponseModel>.Success(model, "Here is the category: ");

        Result:
            return response;
        }

        public async Task<Result<ProductCategoryResponseModel>> ChangeCategoryName(string categoryCode, string newName)
        {
            Result<ProductCategoryResponseModel> response = new Result<ProductCategoryResponseModel>();

            if (String.IsNullOrEmpty(newName) || newName.Length < 3)
            {
                response = Result<ProductCategoryResponseModel>.ValidationError("New Name can't lower than 6 characters!");
                goto Result;
            }

            var category = await _db.TblProductCategoryPos.AsNoTracking().Where(c => c.DeleteFlag == false && c.ProductCategoryCode == categoryCode).FirstOrDefaultAsync();


            if (category is null)
            {
                response = Result<ProductCategoryResponseModel>.NotFound($"Can't find the Category with code {categoryCode}.");
                goto Result;
            }

            category.ProductCategoryName = newName;

            _db.Entry(category).State = EntityState.Modified;
            int result = await _db.SaveChangesAsync();

            if(result < 1)
            {
                response = Result<ProductCategoryResponseModel>.SystemError("Category Update Failed!");
                goto Result;
            }

            ProductCategoryResponseModel model = new ProductCategoryResponseModel
            {
                TblProductCategoryPos = category
            };
            response = Result<ProductCategoryResponseModel>.Success(model, "Here is the updated category: ");

        Result:
            return response;
        }

        public async Task<Result<ProductCategoryResponseModel>> DeleteCategory(string categoryCode)
        {
            Result<ProductCategoryResponseModel> response = new Result<ProductCategoryResponseModel>();

            var category = await _db.TblProductCategoryPos.AsNoTracking().Where(c => c.DeleteFlag == false && c.ProductCategoryCode == categoryCode).FirstOrDefaultAsync();


            if (category is null)
            {
                response = Result<ProductCategoryResponseModel>.NotFound($"Can't find the Category with code {categoryCode}.");
                goto Result;
            }

            category.DeleteFlag = true;

            _db.Entry(category).State = EntityState.Modified;
            int result = await _db.SaveChangesAsync();

            if (result < 1)
            {
                response = Result<ProductCategoryResponseModel>.SystemError("Category Delete Failed!");
                goto Result;
            }

            ProductCategoryResponseModel model = new ProductCategoryResponseModel
            {
                TblProductCategoryPos = category
            };
            response = Result<ProductCategoryResponseModel>.Success( null,"Category Deleted!");

        Result:
            return response;
        }
    }
}
