using System;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend
{
    public partial class GestionVentasBDContext : DbContext
    {
        public GestionVentasBDContext()
        {
        }

        public GestionVentasBDContext(DbContextOptions<GestionVentasBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseNpgsql("DevConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ_Categoria_Nombre")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Codigo).HasMaxLength(300);
                                
                entity.Property(e => e.Costo).HasColumnType("numeric(18,2)");

                entity.Property(e => e.Imagen).HasMaxLength(600);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Precio).HasColumnType("numeric(18,2)")
                    .IsRequired();

                entity.Property(e => e.PrecioPromocional).HasColumnType("numeric(18,2)");

                entity.Property(e => e.Unidad).HasMaxLength(50);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("Fk_Producto_Categoria");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300);
                                
                entity.Property(e => e.TelefonoMovil)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(600); 

                entity.Property(e => e.Complemento)
                    .IsRequired()
                    .HasMaxLength(600);

                entity.Property(e => e.Email).HasMaxLength(600);

                entity.Property(e => e.TelefonoSec).HasMaxLength(50);

                entity.Property(e => e.Documento).HasMaxLength(25);

                entity.Property(e => e.Observacion).HasMaxLength(600);

                entity.Property(e => e.Imagen).HasMaxLength(600);

                entity.Property(e => e.MontoCredito).HasColumnType("numeric(18,2)");


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
