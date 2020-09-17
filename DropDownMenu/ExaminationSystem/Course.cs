using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Course")]
    public class Course
    {
        public int ID { get; set; }

        [MaxLength(20), Required]
        public string Name { get; set; }

        [MaxLength(100), Required]
        public string Description { get; set; }

        [Required, Range(0, 500)]
        public int MaxDegree { get; set; }

        [Required, Range(0, 499)]
        public int MinDegree { get; set; }


        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
