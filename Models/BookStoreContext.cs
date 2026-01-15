using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace WPF_for_bokstore.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookFormat> BookFormats { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<StockBalance> StockBalances { get; set; }

    public virtual DbSet<StockBalanceBygenrePublisherInStore> StockBalanceBygenrePublisherInStores { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<TitlePerAuthor> TitlePerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Efternamn).HasMaxLength(50);
            entity.Property(e => e.Förnamn).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Books__3BF79E031FF9534D");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN13");
            entity.Property(e => e.Language).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release_Date");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.BookFormat).WithMany(p => p.Books)
                .HasForeignKey(d => d.BookFormatId)
                .HasConstraintName("Fk_BookFormatId");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Books__GenreId__48CFD27E");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK__Books__Publisher__628FA481");

            entity.HasMany(d => d.Authors).WithMany(p => p.BooksIsbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BooksAuth__Autho__5535A963"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BooksIsbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BooksAuth__Books__6477ECF3"),
                    j =>
                    {
                        j.HasKey("BooksIsbn13", "AuthorId").HasName("PK__BooksAut__B3586DCC5303EBB0");
                        j.ToTable("BooksAuthor");
                        j.IndexerProperty<string>("BooksIsbn13")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .HasColumnName("BooksISBN13");
                    });
        });

        modelBuilder.Entity<BookFormat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookForm__3214EC07141D5944");

            entity.ToTable("BookFormat");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FormatName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07F38A3B51");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShippingCity).HasMaxLength(100);
            entity.Property(e => e.ShippingCountry).HasMaxLength(100);
            entity.Property(e => e.Zipcode).HasMaxLength(10);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC076A7DD8DD");

            entity.ToTable("Genre");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC0714614790");

            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ShipVia)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0747B0B598");

            entity.Property(e => e.BookIsbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("BookISBN13");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.BookIsbn13Navigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookIsbn13)
                .HasConstraintName("FK__OrderDeta__BookI__03F0984C");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__02FC7413");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishe__3214EC07AF2CCC02");

            entity.ToTable("Publisher");

            entity.Property(e => e.PublisherName).HasMaxLength(100);
            entity.Property(e => e.Website).HasMaxLength(255);
        });

        modelBuilder.Entity<StockBalance>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.BookIsbn13 }).HasName("Stock");

            entity.ToTable("StockBalance");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.BookIsbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("BookISBN13");

            entity.HasOne(d => d.BookIsbn13Navigation).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.BookIsbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISBN_Books");

            entity.HasOne(d => d.Store).WithMany(p => p.StockBalances)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StockBala__Store__5629CD9C");
        });

        modelBuilder.Entity<StockBalanceBygenrePublisherInStore>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StockBalanceBYGenrePublisherInStore");

            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.GenreName).HasMaxLength(30);
            entity.Property(e => e.PublisherName).HasMaxLength(100);
            entity.Property(e => e.StoreName).HasMaxLength(30);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC07C9A4D427");

            entity.ToTable("Store");

            entity.Property(e => e.Address).HasMaxLength(80);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.StoreName).HasMaxLength(30);
        });

        modelBuilder.Entity<TitlePerAuthor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TitlePerAuthor");

            entity.Property(e => e.Name).HasMaxLength(101);
            entity.Property(e => e.StockValue).HasColumnType("decimal(38, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
