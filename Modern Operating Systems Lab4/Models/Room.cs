using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modern_Operating_Systems_Lab4.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Class { get; set; }
        public int Beds { get; set; }
        public string Number { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }

        public Guid FloorId { get; set; }
        public Floor Floor { get; set; }
    }
}