using System;
using System.Collections.Generic;
using Lab4.Models;
using Lab4.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data;

public partial class Db8011Context : DbContext
{
    public Db8011Context()
    {
    }

    public Db8011Context(DbContextOptions<Db8011Context> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ConfigurationBuilder builder = new();
        ///Установка пути к текущему каталогу
        builder.SetBasePath(Directory.GetCurrentDirectory());
        // получаем конфигурацию из файла appsettings.json
        builder.AddJsonFile("appsettings.json");
        // создаем конфигурацию
        IConfigurationRoot configuration = builder.AddUserSecrets<Program>().Build();

        string connectionString = "";
        connectionString = configuration.GetConnectionString("RemoteSQLConnection");

        /// Задание опций подключения
        _ = optionsBuilder
            .UseSqlServer(connectionString)
            .Options;
        optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
    }

    public virtual DbSet<AllPack> AllPacks { get; set; }

    public virtual DbSet<Pack> Packs { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<PlaceInPack> PlaceInPacks { get; set; }

    public virtual DbSet<PlacesType> PlacesTypes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPack> UserPacks { get; set; }

    public virtual DbSet<UserReview> UserReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllPack>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AllPacks");

            entity.Property(e => e.PackName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pack>(entity =>
        {
            entity.HasKey(e => e.PackId).HasName("PK__Packs__FA6765696E168851");

            entity.Property(e => e.PackName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Packs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Packs__UserId__7E02B4CC");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__Places__D5222B6EAF6B0642");

            entity.Property(e => e.GeolocationA).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.GeolocationB).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.PlaceDescription)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.Type).WithMany(p => p.Places)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Places__TypeId__7755B73D");
        });

        modelBuilder.Entity<PlaceInPack>(entity =>
        {
            entity.HasKey(e => e.PipId).HasName("PK__PlaceInP__145FF26CB4ED82D7");

            entity.ToTable("PlaceInPack");

            entity.Property(e => e.PipId).HasColumnName("PIP_Id");

            entity.HasOne(d => d.Pack).WithMany(p => p.PlaceInPacks)
                .HasForeignKey(d => d.PackId)
                .HasConstraintName("FK__PlaceInPa__PackI__01D345B0");

            entity.HasOne(d => d.Place).WithMany(p => p.PlaceInPacks)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK__PlaceInPa__Place__00DF2177");
        });

        modelBuilder.Entity<PlacesType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__PlacesTy__516F03B5161BA694");

            entity.HasIndex(e => e.TypeName, "UQ__PlacesTy__D4E7DFA8238F0DA0").IsUnique();

            entity.Property(e => e.TypeName)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CE5C8979A3");

            entity.Property(e => e.ReviewText).HasMaxLength(2000);

            entity.HasOne(d => d.Place).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK__Reviews__PlaceId__7B264821");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserId__7A3223E8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C19955D56");

            entity.HasIndex(e => e.UserLogin, "UQ__Users__7F8E8D5E0F91CBE9").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserLogin)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserPack>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserPacks");

            entity.Property(e => e.PackName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserLogin)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserReview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserReviews");

            entity.Property(e => e.PlaceDescription)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ReviewText).HasMaxLength(2000);
            entity.Property(e => e.UserLogin)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
