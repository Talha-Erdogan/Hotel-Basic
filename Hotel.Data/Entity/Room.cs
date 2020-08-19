using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hotel.Data.Entity
{
    [Table("Room")]
   public class Room
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [StringLength(50)]
        public string Floor { get; set; }

    }
}
