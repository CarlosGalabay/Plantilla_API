using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using proyecto.API.Entities.Productos;
using proyecto.API.Entities.Roles;
using proyecto.API.Entities.Usuarios;

namespace proyecto.API.Entities;

public partial class NombreDbContext : DbContext
{
    public NombreDbContext()
    {
    }

    public NombreDbContext(DbContextOptions<NombreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("productos_pkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("roles_pkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("usuarios_pkey");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_idrol_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
