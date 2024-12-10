using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MTTKDotNetCore.PointOfSale.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblProductCategoryPos> TblProductCategoryPos { get; set; }

    public virtual DbSet<TblProductPos> TblProductPos { get; set; }

    public virtual DbSet<TblSaleInvoiceDetailPos> TblSaleInvoiceDetailPos { get; set; }

    public virtual DbSet<TblSalePos> TblSalePos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6QTP69L\\MSSQLSERVER2022;Database=DotNetTrainingBatch5;User Id=sa;Password=sa123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProductCategoryPos>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId);

            entity.ToTable("Tbl_ProductCategory_POS");

            entity.Property(e => e.ProductCategoryCode).HasMaxLength(50);
            entity.Property(e => e.ProductCategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblProductPos>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("Tbl_Product_POS");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(50);
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblSaleInvoiceDetailPos>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId);
                
            entity.ToTable("Tbl_SaleInvoiceDetail_POS");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
        });

        modelBuilder.Entity<TblSalePos>(entity =>
        {
            entity.HasKey(e => e.SaleId);

            entity.ToTable("Tbl_Sale_POS");

            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
