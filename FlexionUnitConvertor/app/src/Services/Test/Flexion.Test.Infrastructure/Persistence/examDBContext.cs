using System;
using Flexion.Test.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Flexion.Test.Infrastructure.Persistence
{
    public partial class examDBContext : DbContext
    {
        public examDBContext()
        {
        }

        public examDBContext(DbContextOptions<examDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conversion> Conversion { get; set; }
        public virtual DbSet<ConversionType> ConversionType { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<ExamQuestion> ExamQuestion { get; set; }
        public virtual DbSet<ExamQuestionAnswer> ExamQuestionAnswer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversion>(entity =>
            {
                entity.Property(e => e.ConversionId).HasColumnName("ConversionID");

                entity.Property(e => e.ConversionName).IsUnicode(false);

                entity.Property(e => e.ConversionTypeId).HasColumnName("ConversionTypeID");

                entity.HasOne(d => d.ConversionType)
                    .WithMany(p => p.Conversion)
                    .HasForeignKey(d => d.ConversionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conversion_ConversionType");
            });

            modelBuilder.Entity<ConversionType>(entity =>
            {
                entity.Property(e => e.ConversionTypeId).HasColumnName("ConversionTypeID");

                entity.Property(e => e.ConversionName).IsUnicode(false);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.ExamId).HasColumnName("ExamID");

                entity.Property(e => e.DateCompleted).HasColumnType("datetime");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsComplete).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsGraded).HasDefaultValueSql("((0))");


                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            });

            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.Property(e => e.InputValue).HasColumnName("InputValue");
                entity.Property(e => e.ExamQuestionId).HasColumnName("ExamQuestionID");

                entity.Property(e => e.DestinationConversionId).HasColumnName("DestinationConversionID");

                entity.Property(e => e.ExamId).HasColumnName("ExamID");

                entity.Property(e => e.SourceConversionId).HasColumnName("SourceConversionID");

                entity.HasOne(d => d.DestinationConversion)
                    .WithMany(p => p.ExamQuestionDestinationConversion)
                    .HasForeignKey(d => d.DestinationConversionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamQuestion_DestinationConverstion");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamQuestion)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_ExamQuestion");

                entity.HasOne(d => d.SourceConversion)
                    .WithMany(p => p.ExamQuestionSourceConversion)
                    .HasForeignKey(d => d.SourceConversionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamQuestion_SourceConversion");
            });

            modelBuilder.Entity<ExamQuestionAnswer>(entity =>
            {
                entity.Property(e => e.ExamQuestionAnswerId).HasColumnName("ExamQuestionAnswerID");

                entity.Property(e => e.ExamQuestionId).HasColumnName("ExamQuestionID");

                entity.Property(e => e.IsAnswered).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCorrect).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ExamQuestion)
                    .WithMany(p => p.ExamQuestionAnswer)
                    .HasForeignKey(d => d.ExamQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamQuestionAnswer_ExamQuestion");
            });
        }
    }
}
