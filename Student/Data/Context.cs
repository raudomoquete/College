using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Data
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Grade)
                .WithOne(g => g.Student)
                .HasForeignKey<Grade>(g => g.Id);

            base.OnModelCreating(modelBuilder);
        }
        public Context()
        { 
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<StudentModel> Students { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
    }
}
