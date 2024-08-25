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

    public virtual DbSet<PublicationTable> PublicationTables { get; set; }

    public virtual DbSet<ReplyTable> ReplyTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CSTAVTI\\SQLEXPRESS;Database=Blog;Trusted_Connection=True;TrustServerCertificate=True;");

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

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.ReplyTables)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK_Reply_Publication");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
