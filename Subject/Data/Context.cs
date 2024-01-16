using Microsoft.EntityFrameworkCore;
using Subject.Models;

namespace Subject.Data
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GradeSubject>()
                .HasKey(gs => new { gs.SubjectId, gs.GradeId });

            modelBuilder.Entity<GradeSubject>()
                .HasOne(gs => gs.Grade)
                .WithMany(g => g.Subjects)
                .HasForeignKey(gs => gs.GradeId);

            modelBuilder.Entity<GradeSubject>()
                .HasOne(gs => gs.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(tg => tg.SubjectId);

        }

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<SubjectModel> Subjects { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeSubject> GradeSubjects { get; set; }
        
    }
}
