namespace StudentSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using StudentSystem.Model;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentSystemContext context)
        {
            context.Courses.AddOrUpdate(new Course { Name = "REST" });
            context.Students.AddOrUpdate(new Student { Name = "Pesho", Number = "234234" });
        }
    }
}
