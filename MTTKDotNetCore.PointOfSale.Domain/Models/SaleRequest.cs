using MTTKDotNetCore.PointOfSale.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Models
{
    //public class SaleRequest
    //{
    //    public TblSalePos Sale { get; set;}

    //    public TblSaleInvoiceDetailPos SaleInvoiceDetail { get; set; }
    //}
    public class CreateSaleRequest
    {
        public List<SaleItem> SaleItems { get; set; }

        public class SaleItem
        {
            public string ProductCode { get; set; }
            public int Quantity { get; set; }
        }
    }
}
