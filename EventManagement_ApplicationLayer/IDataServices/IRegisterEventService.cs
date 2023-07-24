using EventManagement_DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_ApplicationLayer.IDataServices
{
	public interface IRegisterEventService
	{
		public Task<List<RegisterEvent>> GetAllRegisterEventsAsync();
		public Task<List<RegisterEvent>> GetAllRegisterEventsByUsernameAsync(string username);
		public Task<RegisterEvent> GetRegisterEventByEventIdAsync(string eventId);
		public Task<RegisterEvent> GetRegisterEventByUsernameAsync(string username);
		public Task<string> AddRegisterEventAsync(RegisterEvent registerEvent);
		//public Task<Event> UpdateRegisterEventAsync(Event eventDetails);
		public Task<string> RemoveRegisterEventAsync(string Id);
	}
}
