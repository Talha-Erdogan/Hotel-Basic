using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hotel.Data.Entity
{
    [Table("Personnel")]
    public class Personnel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(20)]
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

        [StringLength(200)]
        [Required]
        public string Address { get; set; }
    }
}
