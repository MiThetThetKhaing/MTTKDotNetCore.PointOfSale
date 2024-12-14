using MTTKDotNetCore.PointOfSale.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Models
{
    //public class SaleResponseModel
    //{
    //    public TblSalePos TblSalePos { get; set; }

    //    public List<TblSalePos> TblSalePosList { get; set; }

    //    public TblSaleInvoiceDetailPos TblSaleInvoiceDetailPos { get; set; }

    //    public List<TblSaleInvoiceDetailPos> TblSaleInvoiceDetailPosList { get; set; }
    //}

    public class SaleResponseModel
    {
        public string VoucherNo { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItem> SaleItems { get; set; }

        public class SaleItem
        {
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
