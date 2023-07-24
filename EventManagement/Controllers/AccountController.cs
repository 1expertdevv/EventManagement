using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using EventManagement_ApplicationLayer.IDataServices;
using MySqlX.XDevAPI.Common;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Asn1.Cms;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EventManagement_DomainLayer.ViewModals;

namespace EventManagement_API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			var result = await _accountService.RegistrationAsync(model);
			if(result.Contains("successful"))
				return Ok(result);
			else
				return BadRequest(result);
		}

		[AllowAnonymous]
		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginVM model)
		{
			var result = await _accountService.LoginAsync(model);
			if (!result.Contains("User Not Found"))
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

	}
}
