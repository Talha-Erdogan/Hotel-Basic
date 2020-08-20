using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Models.Room
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [StringLength(50)]
        public string Floor { get; set; }

        //edit or delete 
        public string SubmitType { get; set; }
    }
}
