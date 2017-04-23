using System.Data.Entity.ModelConfiguration;
using StudentSystem.Model;

namespace StudentSystem.Data
{
    public class StudentMapping : EntityTypeConfiguration<Student>
    {
        public StudentMapping()
        {
            this.HasKey(x => x.StudentId);
        }
    }
}