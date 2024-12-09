using Microsoft.EntityFrameworkCore;
using MTTKDotNetCore.PointOfSale.Database.Models;
using MTTKDotNetCore.PointOfSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Features;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<ProductResponseModel>> CreateProduct(TblProductPos product)
    {
        try
        {
            var productCode = _db.TblProductPos.AsNoTracking().FirstOrDefault(x => x.ProductCode == product.ProductCode);
            var productCategoryCode = _db.TblProductCategoryPos.AsNoTracking().FirstOrDefault(x => x.ProductCategoryCode == product.ProductCategoryCode);

            if (productCode != null)
            {
                return Result<ProductResponseModel>.ValidationError("Product Code is already exists.");
            }
            if (productCategoryCode is null)
            {
                return Result<ProductResponseModel>.ValidationError("Product Category Code doesn't exist!");
            }
            if (string.IsNullOrEmpty(product.ProductCategoryCode))
            {
                return Result<ProductResponseModel>.ValidationError("Product Category Code can't be blank.");
            }

            await _db.TblProductPos.AddAsync(product);
            await _db.SaveChangesAsync();

            var result = new ProductResponseModel
            {
                TblProductPos = product
            };

            return Result<ProductResponseModel>.Success(result, "Product is successfully created!");
        }
        catch (Exception ex)
        {
            return Result<ProductResponseModel>.SystemError(ex.Message);
        }
    }
}
