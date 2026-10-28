using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Planillas.Infrastructure;

namespace Planillas.Infrastructure.Persistence;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Boletaspago> Boletaspagos { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Detallesboletum> Detallesboleta { get; set; }

    public virtual DbSet<Obra> Obras { get; set; }

    public virtual DbSet<Planilla> Planillas { get; set; }

    public virtual DbSet<Prestamosadelanto> Prestamosadelantos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Trabajadore> Trabajadores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=PlanillasConstructoraDb;Uid=root;Pwd=JorgeSN_2025!;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asistencias");

            entity.HasIndex(e => e.ObraId, "ObraId");

            entity.HasIndex(e => new { e.TrabajadorId, e.Fecha }, "UQ_Asistencia_Trabajador_Fecha").IsUnique();

            entity.Property(e => e.Estado).HasColumnType("enum('Presente','Falta','Tardanza','Permiso','Descanso Medico','Feriado')");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.HorasExtras)
                .HasPrecision(4)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.HorasTrabajadas)
                .HasPrecision(4)
                .HasDefaultValueSql("'8.00'");
            entity.Property(e => e.MinutosTardanza).HasDefaultValueSql("'0'");
            entity.Property(e => e.Observaciones).HasMaxLength(255);

            entity.HasOne(d => d.Obra).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.ObraId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("asistencias_ibfk_2");

            entity.HasOne(d => d.Trabajador).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.TrabajadorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("asistencias_ibfk_1");
        });

        modelBuilder.Entity<Boletaspago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("boletaspago");

            entity.HasIndex(e => e.TrabajadorId, "TrabajadorId");

            entity.HasIndex(e => new { e.PlanillaId, e.TrabajadorId }, "UQ_Boleta_Planilla_Trabajador").IsUnique();

            entity.Property(e => e.DiasFalta).HasDefaultValueSql("'0'");
            entity.Property(e => e.HorasExtrasTotales)
                .HasPrecision(6)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.NetoPagar).HasPrecision(10);
            entity.Property(e => e.RemuneracionBasica).HasPrecision(10);
            entity.Property(e => e.TotalAportesEmpresa)
                .HasPrecision(10)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.TotalDescuentos).HasPrecision(10);
            entity.Property(e => e.TotalIngresos).HasPrecision(10);

            entity.HasOne(d => d.Planilla).WithMany(p => p.Boletaspagos)
                .HasForeignKey(d => d.PlanillaId)
                .HasConstraintName("boletaspago_ibfk_1");

            entity.HasOne(d => d.Trabajador).WithMany(p => p.Boletaspagos)
                .HasForeignKey(d => d.TrabajadorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("boletaspago_ibfk_2");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cargos");

            entity.HasIndex(e => e.Nombre, "Nombre").IsUnique();

            entity.Property(e => e.Area).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.SueldoBaseReferencial).HasPrecision(10);
        });

        modelBuilder.Entity<Detallesboletum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detallesboleta");

            entity.HasIndex(e => e.BoletaPagoId, "BoletaPagoId");

            entity.Property(e => e.Concepto).HasMaxLength(100);
            entity.Property(e => e.Monto).HasPrecision(10);
            entity.Property(e => e.Tipo).HasColumnType("enum('Ingreso','Descuento','Aporte')");

            entity.HasOne(d => d.BoletaPago).WithMany(p => p.Detallesboleta)
                .HasForeignKey(d => d.BoletaPagoId)
                .HasConstraintName("detallesboleta_ibfk_1");
        });

        modelBuilder.Entity<Obra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("obras");

            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Planificacion'")
                .HasColumnType("enum('Planificacion','En Ejecucion','Suspendida','Finalizada')");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFinEstimada).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.PresupuestoManoObra).HasPrecision(12);
            entity.Property(e => e.Ubicacion).HasMaxLength(255);
        });

        modelBuilder.Entity<Planilla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("planillas");

            entity.HasIndex(e => e.Codigo, "Codigo").IsUnique();

            entity.HasIndex(e => e.ObraId, "ObraId");

            entity.HasIndex(e => e.UsuarioProcesoId, "UsuarioProcesoId");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Borrador'")
                .HasColumnType("enum('Borrador','Revisada','Aprobada','Pagada')");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.Tipo).HasColumnType("enum('Semanal','Quincenal','Mensual','Liquidacion')");

            entity.HasOne(d => d.Obra).WithMany(p => p.Planillas)
                .HasForeignKey(d => d.ObraId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("planillas_ibfk_1");

            entity.HasOne(d => d.UsuarioProceso).WithMany(p => p.Planillas)
                .HasForeignKey(d => d.UsuarioProcesoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("planillas_ibfk_2");
        });

        modelBuilder.Entity<Prestamosadelanto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("prestamosadelantos");

            entity.HasIndex(e => e.TrabajadorId, "TrabajadorId");

            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Pendiente'")
                .HasColumnType("enum('Pendiente','Pagando','Cancelado')");
            entity.Property(e => e.FechaSolicitud).HasColumnType("date");
            entity.Property(e => e.MontoPorCuota).HasPrecision(10);
            entity.Property(e => e.MontoTotal).HasPrecision(10);
            entity.Property(e => e.Motivo).HasMaxLength(255);
            entity.Property(e => e.NumeroCuotas).HasDefaultValueSql("'1'");
            entity.Property(e => e.Tipo).HasColumnType("enum('Adelanto','Prestamo')");

            entity.HasOne(d => d.Trabajador).WithMany(p => p.Prestamosadelantos)
                .HasForeignKey(d => d.TrabajadorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("prestamosadelantos_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "Nombre").IsUnique();

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Trabajadore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("trabajadores");

            entity.HasIndex(e => e.CargoId, "CargoId");

            entity.HasIndex(e => e.NumeroDocumento, "NumeroDocumento").IsUnique();

            entity.HasIndex(e => e.ObraActualId, "ObraActualId");

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EstadoLaboral)
                .HasDefaultValueSql("'Activo'")
                .HasColumnType("enum('Activo','De Vacaciones','Descanso Medico','Cesado')");
            entity.Property(e => e.FechaCese).HasColumnType("date");
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.ModalidadPago).HasColumnType("enum('Mensual','Jornal Diario')");
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(20);
            entity.Property(e => e.RemuneracionBase).HasPrecision(10);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.TipoContrato).HasColumnType("enum('Plazo Indeterminado','Plazo Fijo','Por Obra','Recibo por Honorarios')");
            entity.Property(e => e.TipoDocumento)
                .HasDefaultValueSql("'DNI'")
                .HasColumnType("enum('DNI','CE','Pasaporte')");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("trabajadores_ibfk_1");

            entity.HasOne(d => d.ObraActual).WithMany(p => p.Trabajadores)
                .HasForeignKey(d => d.ObraActualId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("trabajadores_ibfk_2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.RolId, "RolId");

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValueSql("'1'");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto).HasMaxLength(150);
            entity.Property(e => e.PasswordHash).HasColumnType("text");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
