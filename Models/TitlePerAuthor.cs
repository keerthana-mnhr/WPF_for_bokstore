using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class TitlePerAuthor
{
    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    public int? Titles { get; set; }

    public decimal? StockValue { get; set; }
}
