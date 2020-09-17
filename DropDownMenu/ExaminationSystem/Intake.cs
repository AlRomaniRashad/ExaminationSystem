using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Intake")]
    public class Intake
    {
        public int ID { get; set; }

        [MaxLength(50), Required]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "You Entered in Valid Intake Name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime Year { get; set; }


        public virtual ICollection<BranchTrackIntake> BranchTrackIntakes { get; set; }
    }
}
