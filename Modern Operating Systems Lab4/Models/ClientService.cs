using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modern_Operating_Systems_Lab4.Models
{
    public class ClientService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsBooking { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }


        public Guid ClientId { get; set; }
        public Client Client { get; set; }


        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}