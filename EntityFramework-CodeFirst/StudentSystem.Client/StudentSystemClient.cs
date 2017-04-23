using StudentSystem.Data;
using StudentSystem.Model;
using System.Data.Entity;
using StudentSystem.Data.Migrations;
using System.Collections.Generic;

namespace StudentSystem.Client
{
    class StudentSystemClient
    {
        static void Main()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion
                    <StudentSystemContext, Configuration>());

            var db = new StudentSystemContext();

            var student = new Student { Name = "Denkata", Number = "21313" };
            db.Students.Add(student);

            var courseStudents = new List<Student>();
            courseStudents.Add(student);
            var course = new Course { Name = "JS Apps", Description = "Cool", Materials = "A lot", Students = courseStudents };
            db.Courses.Add(course);

            db.SaveChanges();
        }
    }
}