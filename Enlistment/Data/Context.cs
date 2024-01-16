using Enlistment.Models;
using Microsoft.EntityFrameworkCore;

namespace Enlistment.Data
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnlistmentModel>()
                .HasOne(e => e.Grade)
                .WithOne(g => g.Enlistment)
                .HasForeignKey<Grade>(g => g.GradeId);

            modelBuilder.Entity<EnlistmentModel>()
              .HasOne(e => e.Student)
              .WithOne(s => s.Enlistment)
              .HasForeignKey<StudentModel>(s => s.StudentId);

            modelBuilder.Entity<EnlistmentModel>()
              .HasOne(e => e.Subject)
              .WithOne(s => s.Enlistment)
              .HasForeignKey<SubjectModel>(s => s.SubjectId);

            base.OnModelCreating(modelBuilder);
        }
        public Context()
        {   
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {  
        }

        public virtual DbSet<EnlistmentModel> Enlistments { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<StudentModel> Students { get; set; }
        public virtual DbSet<SubjectModel> Subjects { get; set; }
    }
}
