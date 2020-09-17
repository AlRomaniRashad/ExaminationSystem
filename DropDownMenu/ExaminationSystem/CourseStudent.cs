using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("CourseStudent")]
    public class CourseStudent
    {
        public int Id { get; set; }

        [ForeignKey("Course"), Required]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        [ForeignKey("Student"), Required]
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}
