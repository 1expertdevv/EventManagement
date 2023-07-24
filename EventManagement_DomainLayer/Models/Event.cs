using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_DomainLayer.Models
{
	public class Event
	{
		[Key]
		[Display(Name = "Event ID")]
		public string EventId { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Title { get; set; } 
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		[Required]
		[Display(Name = "Location")]
		public string TimeZone { get; set; }

		[Required]
		public string Description { get; set; }	
	}
}
