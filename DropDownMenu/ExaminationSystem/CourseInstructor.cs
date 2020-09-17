using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("CourseInstructor")]
    public class CourseInstructor
    {
        public int ID { get; set; }


        [ForeignKey("Branch"), Required]
        public int BranchID { get; set; }
        public virtual Branch Branch { get; set; }


        [ForeignKey("Track"), Required]
        public int TrackID { get; set; }
        public virtual Track Track { get; set; }


        [ForeignKey("Course"), Required]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }


        [ForeignKey("Instructor"), Required]
        public int InstructorID { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
