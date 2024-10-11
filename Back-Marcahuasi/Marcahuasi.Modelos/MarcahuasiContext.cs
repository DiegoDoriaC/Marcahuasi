using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Marcahuasi.Modelos;

public partial class MarcahuasiContext : DbContext
{
    public MarcahuasiContext()
    {
    }

    public MarcahuasiContext(DbContextOptions<MarcahuasiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administrador { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public virtual DbSet<Nacionalidad> Nacionalidades { get; set; }

    public virtual DbSet<TarifaPago> TarifaPagos { get; set; }

    public virtual DbSet<Turista> Turistas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.IdAdministrado).HasName("PK__Administ__EC0181E2B17E513F");

            entity.ToTable("Administrador");

            entity.Property(e => e.Contraceña)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion)
                .IsUnicode (false);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D0B41B8E6");

            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.IdIngreso).HasName("PK__Ingresos__901EF2E388FC4EFE");

            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime");
            entity.Property(e => e.Observacion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.UserRegister)
                .HasMaxLength(180)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTarifaNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.IdTarifa)
                .HasConstraintName("FK__Ingresos__IdTari__5629CD9C");

            entity.HasOne(d => d.IdTuristaNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.IdTurista)
                .HasConstraintName("FK__Ingresos__IdTuri__5535A963");
        });

        modelBuilder.Entity<Nacionalidad>(entity =>
        {
            entity.HasKey(e => e.IdNacionalidad).HasName("PK__Nacional__021E36BE11A6EE08");

            entity.Property(e => e.CodigoIso)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CodigoISO");
            entity.Property(e => e.Pais)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(550)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TarifaPago>(entity =>
        {
            entity.HasKey(e => e.IdTarifa).HasName("PK__TarifaPa__78F1A91DD014624A");

            entity.Property(e => e.MontoTarifa).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NombreTarifa)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Turista>(entity =>
        {
            entity.HasKey(e => e.IdTurista).HasName("PK__Turistas__85203DCA400C6BE8");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(9)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Turista)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Turistas__IdDepa__5070F446");

            entity.HasOne(d => d.IdNacionalidadNavigation).WithMany(p => p.Turista)
                .HasForeignKey(d => d.IdNacionalidad)
                .HasConstraintName("FK__Turistas__IdNaci__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
