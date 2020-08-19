using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Models.Profile
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }

        public int? DeletedBy { get; set; }

        //edit or delete 
        public string SubmitType { get; set; }
    }
}
