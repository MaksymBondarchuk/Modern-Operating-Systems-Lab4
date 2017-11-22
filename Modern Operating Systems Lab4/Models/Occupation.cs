using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modern_Operating_Systems_Lab4.Models
{
	public class Occupation
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public decimal Salary { get; set; }
		public bool IsFrontOffice { get; set; }
	}
}