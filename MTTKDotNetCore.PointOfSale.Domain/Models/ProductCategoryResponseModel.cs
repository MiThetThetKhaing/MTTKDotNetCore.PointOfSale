using MTTKDotNetCore.PointOfSale.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Models
{
    public class ProductCategoryResponseModel
    {
        public TblProductCategoryPos TblProductCategoryPos { get; set; }

        public List<TblProductCategoryPos> TblProductCategoryPosList { get; set; }
    }
}
