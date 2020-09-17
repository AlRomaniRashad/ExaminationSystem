using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("BranchTrackIntake")]
    public class BranchTrackIntake
    {
        public int ID { get; set; }


        [ForeignKey("Branch")]
        public int BranchID { get; set; }
        public virtual Branch Branch { get; set; }

        [ForeignKey("Track")]
        public int TrackID { get; set; }
        public virtual Track Track { get; set; }

        [ForeignKey("Intake")]
        public int IntakeID { get; set; }
        public virtual Intake Intake { get; set; }


        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }

    }
}
