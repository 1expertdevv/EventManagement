using EventManagement_ApplicationLayer.IDataServices;
using EventManagement_DomainLayer.Models;
using EventManagement_InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_InfrastructureLayer.DataServices
{
	public class EventService : IEventService
	{
		private readonly EventContext _context;
		public EventService(EventContext context)
		{
			_context = context;
		}

		public async Task<Event> AddEventAsync(Event eventDetails)
		{
			eventDetails.EventId = Guid.NewGuid().ToString();
			try
			{
				await _context.Events.AddAsync(eventDetails);
				if (await _context.SaveChangesAsync() > 0)
					return await _context.Events.FindAsync(eventDetails.EventId);
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		public async Task<List<Event>> GetAllEventsAsync()
		{
			return await _context.Events.ToListAsync();
		}

		public async Task<List<Event>> GetAllEventsByUsernameAsync(string username)
		{
			return await _context.Events.Where(x =>x.Username == username).ToListAsync();
		}

		public async Task<Event> GetEventAsync(string eventId)
		{
			return await _context.Events.FindAsync(eventId);
		}

		public async Task<bool> RemoveEventAsync(string eventId)
		{
			bool result = true;
			var eventDtls = await _context.Events.FindAsync(eventId);

			try
			{
				_context.Remove(eventDtls);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{

				result = false;
			}
			return result;
		}

		public async Task<Event> UpdateEventAsync(Event eventDetails)
		{
			_context.Events.Update(eventDetails);
			try
			{
				if (await _context.SaveChangesAsync() > 0)
					return await _context.Events.FindAsync(eventDetails.EventId);
			}
			catch (Exception ex)
			{
			}
			return null;

		}
	}
}
