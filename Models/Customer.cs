using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? CustomerName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? ShippingCity { get; set; }

    public string? ShippingCountry { get; set; }

    public string? Zipcode { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
