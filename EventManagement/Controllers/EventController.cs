using EventManagement_ApplicationLayer.IDataServices;
using EventManagement_DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Tracing;
using System.Drawing.Printing;


namespace EventManagement_API.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEventService _eventService;
		private readonly IEmailService _emailService;
		private const int EventsPerPage = 5;

		public EventController(IEventService eventService, IEmailService emailService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_eventService = eventService;
			_emailService = emailService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEvents(int page = 1, int pageSize = EventsPerPage)
		{
			var allEvents = await _eventService.GetAllEventsAsync();
			if (allEvents.Count > 0)
			{
				int startIndex = (page - 1) * pageSize;
				var paginatedEvents = allEvents.Skip(startIndex).Take(pageSize).ToList();
				double eventCount = allEvents.Count();
				var response = new
				{
					Page = page,
					TotalPages = (int)Math.Ceiling(eventCount / pageSize),
					Events = paginatedEvents
				};
				return Ok(response);
			}
			return BadRequest();
		}

		[HttpGet("GetAllEventsByUsername")]
		public async Task<IActionResult> GetAllEventsByUsername(int page = 1, int pageSize = EventsPerPage)
		{
			string userName = string.Empty;
			if (_signInManager.IsSignedIn(User))
			{
				userName = _userManager.GetUserName(User);
			}
			var allEvents = await _eventService.GetAllEventsByUsernameAsync(userName);
			if (allEvents.Count > 0)
			{
				int startIndex = (page - 1) * pageSize;
				var paginatedEvents = allEvents.Skip(startIndex).Take(pageSize).ToList();
				double eventCount = allEvents.Count();
				var response = new
				{
					Page = page,
					TotalPages = (int)Math.Ceiling(eventCount / pageSize),
					Events = paginatedEvents
				};
				return Ok(response);
			}
			return BadRequest();
		}


		[HttpGet("{eventId}")]
		public async Task<IActionResult> GetEvent(string eventId)
		{
			var Event = await _eventService.GetEventAsync(eventId);
			if (Event != null)
			{
				return Ok(Event);
			}
			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> AddEvent(Event eventDetail)
		{
			if (_signInManager.IsSignedIn(User))
			{
				eventDetail.Username = _userManager.GetUserName(User);
			}
			var Event = await _eventService.AddEventAsync(eventDetail);
			if (Event != null)
				return Ok(Event);
			return BadRequest();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateEvent(Event eventDetail)
		{
			var Event = await _eventService.UpdateEventAsync(eventDetail);
			if (Event != null)
				return Ok(Event);
			return BadRequest();
		}
	
		[HttpDelete("{eventId}")]
		public Task<bool> DeleteEvent(string eventId)
		{
			return _eventService.RemoveEventAsync(eventId);
		}
	}
}
