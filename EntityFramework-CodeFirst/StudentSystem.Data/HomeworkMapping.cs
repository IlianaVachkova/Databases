using System.Data.Entity.ModelConfiguration;
using StudentSystem.Model;

namespace StudentSystem.Data
{
    public class HomeworkMapping : EntityTypeConfiguration<Homework>
    {
        public HomeworkMapping()
        {
            this.HasKey(x => x.HomeworkId);
        }
    }
}