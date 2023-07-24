using EventManagement_ApplicationLayer.IDataServices;
using EventManagement_DomainLayer.Models;
using EventManagement_DomainLayer.ViewModals;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_InfrastructureLayer.DataServices
{
    public class AccountService : IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _configuration;


		public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}
		public async Task<string> LoginAsync(LoginVM Input)
		{
			string Message = "User Not Found";

			var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
			if (result.Succeeded)
			{

				var jwtSecretKey = _configuration["JWT:Key"];
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
				var tokenHandler = new JwtSecurityTokenHandler();
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
					new Claim(ClaimTypes.Name, Input.Email)
					}),
					Expires = DateTime.UtcNow.AddDays(1),
					SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				return tokenString;
			}
			return Message;
		}

		public async Task<string> RegistrationAsync(RegisterVM modal)
		{
			var Message = "Registration Failed\n\n";
			var user = CreateUser();
			user.Email = modal.Email;
			user.UserName = modal.Email;

			var result = await _userManager.CreateAsync(user, modal.Password);

			if (result.Succeeded)
				Message = "Registration successful";
			else
				foreach (var error in result.Errors)
					Message += error.Description + "\n";
			return Message;
		}

		private ApplicationUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<ApplicationUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
					$"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}
	}
}
