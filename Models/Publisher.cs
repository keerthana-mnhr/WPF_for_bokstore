using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string PublisherName { get; set; } = null!;

    public string? Website { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
