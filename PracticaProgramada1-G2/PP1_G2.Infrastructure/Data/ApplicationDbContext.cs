using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PP1_G2.Domain.Entidades;

namespace PP1_G2.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Direcciones> Direcciones { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Clientes_Email").IsUnique();

            entity.HasIndex(e => e.Email, "idx_clientes_email");

            entity.HasIndex(e => new { e.Nombre, e.Apellido }, "idx_clientes_nombre");

            entity.Property(e => e.Activo).HasDefaultValue(1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.FechaModificacion).HasColumnType("DATETIME");
            entity.Property(e => e.FechaNacimiento).HasColumnType("DATE");
        });

        modelBuilder.Entity<Direcciones>(entity =>
        {
            entity.HasKey(e => e.IdDireccion);

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("datetime('Now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.FechaModificacion).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.ToTable("Telefonos");
            entity.HasKey(e => e.IdTelefono);
            entity.Property(e => e.Numero).IsRequired();
            entity.HasOne(e => e.Cliente)
                .WithMany(e => e.Telefonos)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
