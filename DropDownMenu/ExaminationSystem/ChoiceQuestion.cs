using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("ChoiceQuestion")]
    public class ChoiceQuestion
    {
        public int ID { get; set; }

        [MaxLength(100), Required(ErrorMessage = "You Have to fill Choice field")]
        public string Choice { get; set; }


        [Required(ErrorMessage = "You Have to specify is this Choice True or Not!")]
        public bool IsTrue { get; set; }


        [ForeignKey("Question"), Required]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
