using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PublicationTable> Publications { get; set; }

    public virtual DbSet<ReplyTable> Replies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublicationTable>(entity =>
        {
            entity.HasKey(e => e.IdPublication).HasName("PK__publicat__ECEE91EE6D06DBBE");

            entity.ToTable("publicationTable");

            entity.Property(e => e.IdPublication).HasColumnName("idPublication");
            entity.Property(e => e.Content)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("content");
        });

        modelBuilder.Entity<ReplyTable>(entity =>
        {
            entity.HasKey(e => e.IdReply).HasName("PK__replyTab__86C782A9E506B731");

            entity.ToTable("replyTable");

            entity.Property(e => e.IdReply).HasColumnName("idReply");
            entity.Property(e => e.Content)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.IdPublication).HasColumnName("idPublication");

            entity.HasOne(d => d.IdPublicationNavigation)
                .WithMany(p => p.ReplyTables)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK_Reply_Publication");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

