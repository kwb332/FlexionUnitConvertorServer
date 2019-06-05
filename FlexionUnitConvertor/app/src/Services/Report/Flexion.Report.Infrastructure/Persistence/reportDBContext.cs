using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Flexion.Report.API
{
    public partial class ReportDBContext : DbContext
    {
        public ReportDBContext()
        {
        }

        public ReportDBContext(DbContextOptions<ReportDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Infrastructure.DataModel.Report> Report { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Infrastructure.DataModel.Report>(entity =>
            {
                entity.Property(e => e.ReportId)
                    .HasColumnName("ReportID");

                entity.Property(e => e.ExamDate).HasColumnType("datetime");

                entity.Property(e => e.ExamDescription).IsUnicode(false);

                entity.Property(e => e.ExamId).HasColumnName("ExamID");

                entity.Property(e => e.InputUnitOfMeasure).IsUnicode(false);

                entity.Property(e => e.OutPutUnitOfMeasure).IsUnicode(false);

                entity.Property(e => e.StudentName).IsUnicode(false);

                entity.Property(e => e.TeacherName).IsUnicode(false);
            });
        }
    }
}
