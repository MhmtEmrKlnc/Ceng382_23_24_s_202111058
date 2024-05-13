using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Models;

public partial class LabWebAppDbContext : DbContext
{
    public LabWebAppDbContext()
    {
    }

    public LabWebAppDbContext(DbContextOptions<LabWebAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblLog> TblLogs { get; set; }

    public virtual DbSet<TblReservation> TblReservations { get; set; }

    public virtual DbSet<TblRoom> TblRooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("MyConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_log__3213E83F295C4F67");

            entity.ToTable("tbl_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("action");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.RoomId).HasColumnName("roomId");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userId");

            entity.HasOne(d => d.Room).WithMany(p => p.TblLogs)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__tbl_log__roomId__5EBF139D");
        });

        modelBuilder.Entity<TblReservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_rese__3213E83FB7576CF4");

            entity.ToTable("tbl_reservation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ReservationEndDate)
                .HasColumnType("datetime")
                .HasColumnName("reservationEndDate");
            entity.Property(e => e.ReservationStartDate)
                .HasColumnType("datetime")
                .HasColumnName("reservationStartDate");
            entity.Property(e => e.RoomId).HasColumnName("roomId");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userId");

            entity.HasOne(d => d.Room).WithMany(p => p.TblReservations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__tbl_reser__roomI__619B8048");
        });

        modelBuilder.Entity<TblRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_room__3213E83F8AE044BD");

            entity.ToTable("tbl_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.PricePerNight).HasColumnName("pricePerNight");
            entity.Property(e => e.RoomName)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("roomName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
