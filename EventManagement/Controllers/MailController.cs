using EventManagement_ApplicationLayer.IDataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MailController : ControllerBase
	{
		private readonly IEmailService _emailService;
		public MailController(IEmailService emailService)
		{
			_emailService = emailService;
		}


		[HttpPost]
		public async Task<IActionResult> SendInventation(string[] recipientEmails, string eventId)
		{
			return Ok(await _emailService.SendInvitationAsync(recipientEmails, eventId));
		}
	}
}
