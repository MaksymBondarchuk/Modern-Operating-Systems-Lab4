using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modern_Operating_Systems_Lab4.Models
{
    public class ClientAccommodation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsBooking { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }


        public Guid ClientId { get; set; }
        public Client Client { get; set; }


        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}