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
	public class RegisterEventService : IRegisterEventService
	{
		private readonly EventContext _context;
		public RegisterEventService(EventContext context)
		{
			_context = context;
		}

		public async Task<string> AddRegisterEventAsync(RegisterEvent registerEvent)
		{
			string message = "Failed";
			try
			{
				registerEvent.Id = Guid.NewGuid().ToString();
				await _context.RegisterEvents.AddAsync(registerEvent);
				if (await _context.SaveChangesAsync() > 0)
					message = "SuccessFully Added";
				return message;
			}
			catch (Exception ex)
			{
			}
			return message;
		}

		public async Task<List<RegisterEvent>> GetAllRegisterEventsAsync()
		{
			return await _context.RegisterEvents.ToListAsync();
		}

		public async Task<List<RegisterEvent>> GetAllRegisterEventsByUsernameAsync(string username)
		{
			return await _context.RegisterEvents.Where(x =>x.Username == username).ToListAsync();
		}

		public async Task<RegisterEvent> GetRegisterEventByEventIdAsync(string eventId)
		{
			return await _context.RegisterEvents.FindAsync(eventId);
		}

		public async Task<RegisterEvent> GetRegisterEventByUsernameAsync(string username)
		{
			return await _context.RegisterEvents.FindAsync(username);
		}

		public async Task<string> RemoveRegisterEventAsync(string Id)
		{
			string message = "Failed";
			var registerEvent = await _context.RegisterEvents.FindAsync(Id);

			try
			{
				_context.Remove(registerEvent);
				await _context.SaveChangesAsync();
				message = "Successful";
				return message;
			}
			catch (Exception)
			{
			}
			return message;
		}
	}
}
