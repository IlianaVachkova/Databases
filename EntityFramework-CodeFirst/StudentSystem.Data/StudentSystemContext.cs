using System.Data.Entity;
using StudentSystem.Model;

namespace StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("StudentSystemDB")
        { }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseMapping());
            modelBuilder.Configurations.Add(new StudentMapping());
            modelBuilder.Configurations.Add(new HomeworkMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
