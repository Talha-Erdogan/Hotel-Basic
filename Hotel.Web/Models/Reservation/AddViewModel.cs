using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Models.Reservation
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int TotalDebt { get; set; }

        //room price information
        public int RoomPrice { get; set; }

        //other
        public int RoomTotalDept { get; set; }
        public string Customer_Name { get; set; }

        //select list
        public List<SelectListItem> CustomerSelectList { get; set; }
    }
}
