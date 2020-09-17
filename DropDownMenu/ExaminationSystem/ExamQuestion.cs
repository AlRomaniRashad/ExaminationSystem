using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("ExamQuestion")]
    public class ExamQuestion
    {
        public int ID { get; set; }

        [Required]
        public int QuestionMark { get; set; }


        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }


        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }


        public virtual ICollection<StudentExamQuestion> StudentExamQuestions { get; set; }
    }
}
