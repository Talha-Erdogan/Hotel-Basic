using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hotel.Data.Entity
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int TotalDebt { get; set; }
    }
}
