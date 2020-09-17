using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("TextQuestion")]
    public class TextQuestion
    {
        public int ID { get; set; }

        [Required]
        public string Answer { get; set; }


        [ForeignKey("Question"), Required]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
