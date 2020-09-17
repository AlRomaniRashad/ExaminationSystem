using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Branch")]
    public class Branch
    {

        public int ID { get; set; }

        [MaxLength(50), Required(ErrorMessage = "You Have to fill Name field")]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "You Entered in Valid Branch Name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }


        public virtual ICollection<BranchTrackIntake> BranchTrackIntakes { get; set; }
    }
}
