using System;
using System.Collections.Generic;

namespace TB5.WebAPI.Database.AppDbContextModels;

public partial class TblSale
{
    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime? SaleDate { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public bool IsDelete { get; set; }
}
