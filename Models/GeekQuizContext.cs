using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GeekQuiz.Models;

public partial class GeekQuizContext : DbContext
{
    public GeekQuizContext()
    {
    }

    public GeekQuizContext(DbContextOptions<GeekQuizContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GeekQuiz;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B02F15D11");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__4A4E74C82DF1C2D9");

            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FACA84FFD41");

            entity.Property(e => e.Question1).HasColumnName("Question");

            entity.HasOne(d => d.Category).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Question_Category");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.ScoreId).HasName("PK__Scores__7DD229D1411795DF");

            entity.Property(e => e.PlayedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Scores)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Score_Category");

            entity.HasOne(d => d.Player).WithMany(p => p.Scores)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Score_Player");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
