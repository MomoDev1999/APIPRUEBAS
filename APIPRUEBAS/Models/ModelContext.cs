using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIPRUEBAS.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##USUARIO")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("SYS_C008470");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.Idcategoria)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IDCATEGORIA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("SYS_C008471");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.Idproducto)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IDPRODUCTO");
            entity.Property(e => e.Codigobarra)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CODIGOBARRA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Idcategoria)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IDCATEGORIA");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MARCA");
            entity.Property(e => e.Precio)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRECIO");

            entity.HasOne(d => d.oCategoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .HasConstraintName("FK_IDCATEGORIA");
        });
        modelBuilder.HasSequence("CATEGORIA_SEQ");
        modelBuilder.HasSequence("PRODUCTO_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
