using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class StockBalanceBygenrePublisherInStore
{
    public int Id { get; set; }

    public string StoreName { get; set; } = null!;

    public string? City { get; set; }

    public string? GenreName { get; set; }

    public string PublisherName { get; set; } = null!;

    public int? StockBalance { get; set; }
}
