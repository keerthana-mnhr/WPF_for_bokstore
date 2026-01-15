using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class Book
{
    public string Isbn13 { get; set; } = null!;

    public string? Title { get; set; }

    public string? Language { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? PublisherId { get; set; }

    public int? GenreId { get; set; }

    public int? BookFormatId { get; set; }

    public virtual BookFormat? BookFormat { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
