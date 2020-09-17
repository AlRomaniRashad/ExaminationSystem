//using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    [Table("Login")]
    public class Login
    {
        public int ID { get; set; }

        [MaxLength(50), Required(ErrorMessage = "You Have to fill UserName field")]
        [Index(IsUnique = true)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "You Entered Invalid User Name")]
        public string UserName { get; set; }


        [MaxLength(50), Required(ErrorMessage = "You Have to fill Password field")]
        public string Password { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }


        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
