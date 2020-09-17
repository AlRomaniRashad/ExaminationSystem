using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Student")]
    public class Student
    {
        public int ID { get; set; }

        [MaxLength(50), Required(ErrorMessage = "You Have to fill Name field")]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "You Entered Invalid Student Name")]
        public string Name { get; set; }


        [ForeignKey("Login")]
        public int LoginID { get; set; }
        public virtual Login Login { get; set; }


        [ForeignKey("BranchTrackIntake")]
        public int BranchTrackIntakeID { get; set; }
        public virtual BranchTrackIntake BranchTrackIntake { get; set; }


        public virtual ICollection<CourseStudent> CourseStudents { get; set; }

        public virtual ICollection<StudentExam> StudentExams { get; set; }
    }
}
