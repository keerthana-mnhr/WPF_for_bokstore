using System;
using System.Collections.Generic;

namespace WPF_for_bokstore.Models;

public partial class Author
{
    public int Id { get; set; }

    public string Förnamn { get; set; } = null!;

    public string? Efternamn { get; set; }

    public DateTime? Födelsedatum { get; set; }

    public virtual ICollection<Book> BooksIsbn13s { get; set; } = new List<Book>();
}
