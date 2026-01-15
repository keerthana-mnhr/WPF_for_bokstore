using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class StockBalance
{
    public int StoreId { get; set; }

    public string BookIsbn13 { get; set; } = null!;

    public int? Count { get; set; }

    public virtual Book BookIsbn13Navigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
