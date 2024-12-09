using System;
using System.Collections.Generic;

namespace MTTKDotNetCore.PointOfSale.Database.Models;

public partial class TblSalePos
{
    public int SaleId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }
}
