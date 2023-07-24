using EventManagement_DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_ApplicationLayer.IDataServices
{
	public interface IEventService
	{
		public Task<List<Event>> GetAllEventsAsync();
		public Task<List<Event>> GetAllEventsByUsernameAsync(string username);
		public Task<Event> GetEventAsync(string eventId);
		public Task<Event> AddEventAsync(Event eventDetails);
		public Task<Event> UpdateEventAsync(Event eventDetails);
		public Task<bool> RemoveEventAsync(string eventId);
	}
}
