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

    public async Task<Result<ProductResponseModel>> GetProduct(string productCode)
    {
        Result<ProductResponseModel> model = new Result<ProductResponseModel>();

        var product = await _db.TblProductPos.FirstOrDefaultAsync(x => x.ProductCode == productCode && !x.DeleteFlag);

        if (product is null)
        {
            model = Result<ProductResponseModel>.ValidationError("Product not found or has been deleted.");
            goto Result;
        }

        var Response = new ProductResponseModel
        {
            TblProductPos = product
        };

        model = Result<ProductResponseModel>.Success(Response, "Success.");

    Result:
        return model;
    }


    public async Task<Result<ProductResponseModel>> UpdateProduct(string productCode, TblProductPos updatedProduct)
    {
        Result<ProductResponseModel> model = new Result<ProductResponseModel>();

        var product = await _db.TblProductPos.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == productCode);
        
        if (product is null)
        {
            model = Result<ProductResponseModel>.ValidationError("Product not found.");
            goto Result;
        }

        product.ProductName = updatedProduct.ProductName;
        product.Price = updatedProduct.Price;

        _db.TblProductPos.Update(product); 
        await _db.SaveChangesAsync();

        var Response = new ProductResponseModel
        {
            TblProductPos = product
        };
        model = Result<ProductResponseModel>.Success(Response, "Product updated successfully.");
    Result:
        return model;
    }

    public async Task<Result<ProductResponseModel>> DeleteProduct(string productCode)
    {
        Result<ProductResponseModel> model = new Result<ProductResponseModel>();

        var product = await _db.TblProductPos.FirstOrDefaultAsync(x => x.ProductCode == productCode);

        if (product is null)
        {
            model = Result<ProductResponseModel>.ValidationError("Product not found.");
            goto Result;
        }

        product.DeleteFlag = true;

        _db.TblProductPos.Update(product);
        await _db.SaveChangesAsync();

        var deletedProductResponse = new ProductResponseModel
        {
            TblProductPos = product
        };

        model = Result<ProductResponseModel>.Success(deletedProductResponse, "Product marked as deleted successfully.");

    Result:
        return model;
    }

}
