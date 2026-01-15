using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime? DateOfPurchase { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? ShippedDate { get; set; }

    public string? ShipVia { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
