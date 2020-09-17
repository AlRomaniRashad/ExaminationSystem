using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Exam")]
    public class Exam
    {
        public int ID { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public int ExamDegree { get; set; }

        [MaxLength(20), Required]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "You Entered Invalid Type Name")]
        public string ExamType { get; set; }

        public int TotalTime
        {
            get
            {
                return EndTime.Subtract(StartTime).Hours;
            }
        }


        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }


        [ForeignKey("BranchTrackIntake")]
        public int BranchTrackIntakeId { get; set; }
        public virtual BranchTrackIntake BranchTrackIntake { get; set; }


        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
        public virtual ICollection<StudentExam> StudentExams { get; set; }
    }
}
