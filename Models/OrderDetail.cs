using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public string? BookIsbn13 { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Book? BookIsbn13Navigation { get; set; }

    public virtual Order? Order { get; set; }
}
