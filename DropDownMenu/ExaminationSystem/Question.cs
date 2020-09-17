using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Question")]
    public class Question
    {
        public int ID { get; set; }

        [MaxLength(20), Required]
        public string TypeQuestion { get; set; }

        [MaxLength(100), Required]
        public string Content { get; set; }


        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }


        public virtual ICollection<ChoiceQuestion> ChoiceQuestions { get; set; }
        //public virtual ICollection<TrueFalseQuestion> TrueFalseQuestions { get; set; }
        //public virtual ICollection<TextQuestion> TextQuestions { get; set; }


        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}
