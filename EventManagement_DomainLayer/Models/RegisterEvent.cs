using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_DomainLayer.Models
{
	public class RegisterEvent
	{
		[Key]
		public string Id { get; set; }

		[Required]
		[Display(Name = "Event ID")]
		public string EventId { get; set; }

		[Required]
		public string Username { get; set; }
	}
}
