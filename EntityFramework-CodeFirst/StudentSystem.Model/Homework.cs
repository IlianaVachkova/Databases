using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem.Model
{
    public class Homework
    {
        public int HomeworkId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public virtual Course Course { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Student Student { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
    }
}
