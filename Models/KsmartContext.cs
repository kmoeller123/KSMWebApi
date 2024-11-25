using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KSMWebApi.Models;

public partial class KsmartContext : DbContext
{
    public KsmartContext()
    {
    }

    public KsmartContext(DbContextOptions<KsmartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArtObject> ArtObjects { get; set; }
    public virtual DbSet<ItemViews> ItemViews { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=ksmsqldb.database.windows.net;Database=KSMART;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Authentication=ActiveDirectoryInteractive;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtObject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0749B4AE18");

            entity.ToTable("Art Object");

            entity.Property(e => e.FileName).HasMaxLength(50);
            entity.Property(e => e.FileType)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.LastViewed).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("smallmoney");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
