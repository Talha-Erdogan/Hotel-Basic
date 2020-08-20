using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Models.Customer
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [StringLength(11)]
        [Required]
        public string TC { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(20)]
        [Required]
        public string Phone { get; set; }


        //edit or delete 
        public string SubmitType { get; set; }
    }
}
