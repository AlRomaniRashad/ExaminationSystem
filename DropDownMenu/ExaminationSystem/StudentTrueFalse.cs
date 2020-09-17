using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("StudentTrueFalseAnswer")]
    public class StudentTrueFalseAnswer
    {
        public int ID { get; set; }
        
        [Required]
        public bool TrueFalseAnswer { get; set; }


        [ForeignKey("StudentExamQuestion")]
        public int StudentExamQuestionID { get; set; }
        public virtual StudentExamQuestion StudentExamQuestion { get; set; }
    }
}