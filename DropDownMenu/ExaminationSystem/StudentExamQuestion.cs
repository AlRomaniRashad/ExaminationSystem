using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("StudentExamQuestion")]
    public class StudentExamQuestion
    {
        public int ID { get; set; }

        public int? StudentQuestionMark { get; set; }


        [ForeignKey("ExamQuestion")]
        public int ExamQuestionId { get; set; }
        public virtual ExamQuestion ExamQuestion { get; set; }


        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }


        public virtual ICollection<StudentChoiceAnswer> StudentChoiceAnswers { get; set; }
        public virtual ICollection<StudentTrueFalseAnswer> StudentTrueFalses { get; set; }
        public virtual ICollection<StudentTextAnswer> StudentTexts { get; set; }
    }
}
