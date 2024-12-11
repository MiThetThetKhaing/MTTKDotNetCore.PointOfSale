using MTTKDotNetCore.PointOfSale.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Models
{
    public class SaleRequest
    {
        public TblSalePos Sale { get; set;}

        public TblSaleInvoiceDetailPos SaleInvoiceDetail { get; set; }
    }
}
