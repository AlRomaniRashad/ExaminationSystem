using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Track")]
    public class Track
    {
        public int ID { get; set; }


        [MaxLength(50), Required]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "You Entered in Valid Track Name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }


        public virtual ICollection<BranchTrackIntake> BranchTrackIntakes { get; set; }
    }
}
