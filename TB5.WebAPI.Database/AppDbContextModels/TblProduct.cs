using System;
using System.Collections.Generic;

namespace TB5.WebAPI.Database.AppDbContextModels;

public partial class TblProduct
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public bool IsDelete { get; set; }
}
