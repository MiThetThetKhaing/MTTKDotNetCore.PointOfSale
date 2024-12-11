using MTTKDotNetCore.PointOfSale.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Models
{
    public class SaleResponseModel
    {
        public TblSalePos TblSalePos { get; set; }

        public List<TblSalePos> TblSalePosList { get; set; }

        public TblSaleInvoiceDetailPos TblSaleInvoiceDetailPos { get; set; }

        public List<TblSaleInvoiceDetailPos> TblSaleInvoiceDetailPosList { get; set; }
    }
}
