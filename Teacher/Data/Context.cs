using Microsoft.EntityFrameworkCore;
using Teacher.Models;

namespace Teacher.Data
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherGrade>()
                .HasKey(tg => new { tg.TeacherId, tg.GradeId });

            modelBuilder.Entity<TeacherGrade>()
                .HasOne(tg => tg.Teacher)
                .WithMany(t => t.Grades)
                .HasForeignKey(tg => tg.TeacherId);

            modelBuilder.Entity<TeacherGrade>()
                .HasOne(tg => tg.Grade)
                .WithMany(g => g.Teachers)
                .HasForeignKey(tg => tg.GradeId);

        }

        public class TeacherGrade
        {
            public Guid TeacherId { get; set; }
            public TeacherModel Teacher { get; set; }
            public Guid GradeId { get; set; }
            public Grade Grade { get; set; }
        }

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<TeacherModel> Teachers { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<TeacherGrade> TeacherGrades { get; set; }
    }
}
