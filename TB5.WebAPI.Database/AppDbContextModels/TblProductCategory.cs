using System;
using System.Collections.Generic;

namespace TB5.WebAPI.Database.AppDbContextModels;

public partial class TblProductCategory
{
    public int ProductCategoryId { get; set; }

    public string ProductCategoryName { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public bool IsDelete { get; set; }
}
