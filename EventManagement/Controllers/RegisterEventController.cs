using EventManagement_ApplicationLayer.IDataServices;
using EventManagement_DomainLayer.Models;
using EventManagement_InfrastructureLayer.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_API.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class RegisterEventController : ControllerBase
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRegisterEventService _registerEventService;
		private readonly IEmailService _emailService;

		public RegisterEventController(IRegisterEventService registerEventService, IEmailService emailService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_registerEventService = registerEventService;
			_emailService = emailService;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRegisterEvents()
		{
			var RegisterEvents = await _registerEventService.GetAllRegisterEventsAsync();
			if (RegisterEvents != null)
				return Ok(RegisterEvents);
			return BadRequest();
		}

		[HttpGet("GetAllRegisterEventsByUsername/{username}")]
		public async Task<IActionResult> GetAllRegisterEventsByUsername(string username)
		{
			string userName = string.Empty;
			if (_signInManager.IsSignedIn(User))
			{
				userName = _userManager.GetUserName(User);
			}
			var RegisterEvents = await _registerEventService.GetAllRegisterEventsByUsernameAsync(userName);

			if (RegisterEvents != null)
				return Ok(RegisterEvents);
			return BadRequest();
		}

		
		[HttpGet("{eventId}")]
		public async Task<IActionResult> GetRegisterEventByEventId(string eventId)
		{
			var RegisterEvent = await _registerEventService.GetRegisterEventByEventIdAsync(eventId);
			if (RegisterEvent != null)
				return Ok(RegisterEvent);
			return BadRequest("Data Not Found");
		}

		
		[HttpGet("{username}")]
		public async Task<IActionResult> GetRegisterEventByUsername(string username)
		{
			var RegisterEvent = await _registerEventService.GetRegisterEventByUsernameAsync(username);
			if (RegisterEvent != null)
				return Ok(RegisterEvent);
			return BadRequest("Data Not Found");
		}

		[HttpPost]
		public async Task<IActionResult> AddRegisterEvent(RegisterEvent registerEvent)
		{
			
			string username = string.Empty;
			if (_signInManager.IsSignedIn(User))
			{
				username = _userManager.GetUserName(User);
				registerEvent.Username = username;
			}

			return Ok(await _registerEventService.AddRegisterEventAsync(registerEvent));
		}

		
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEvent(string id)
		{
			return Ok(await _registerEventService.RemoveRegisterEventAsync(id));
		}
	}
}
