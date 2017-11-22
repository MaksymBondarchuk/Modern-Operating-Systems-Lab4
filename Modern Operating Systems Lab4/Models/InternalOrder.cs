using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modern_Operating_Systems_Lab4.Models
{
    public class InternalOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Guid OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }

        public Guid RequestedById { get; set; }
        public Employee RequestedBy { get; set; }

        public Guid AssignedToId { get; set; }
        public Employee AssignedTo { get; set; }
    }
}