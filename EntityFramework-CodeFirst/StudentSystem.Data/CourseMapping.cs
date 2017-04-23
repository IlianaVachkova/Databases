using System.Data.Entity.ModelConfiguration;
using StudentSystem.Model;

namespace StudentSystem.Data
{
    public class CourseMapping : EntityTypeConfiguration<Course>
    {
        public CourseMapping()
        {
            this.HasKey(x => x.CourseId);
        }
    }
}
