﻿using DemoApiApp.Model;
using DemoApiApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoApiApp.Controllers
{
	[Route("api/token")]
	[ApiController]
	public class TokenController : Controller
	{
		public IConfiguration _configuration;
		private readonly DataContext _context;

		public TokenController(IConfiguration config, DataContext context)
		{
			_configuration = config;
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> Post(UserDemo _userData)
		{
			if (_userData != null && _userData.UserName != null && _userData.EmailAddress != null)
			{
				UserDemo user = _context.Users.Where(p => p.Id == 1).FirstOrDefault(); ;

				if (user != null)
				{
					//create claims details based on the user information
					var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
						new Claim("UserId", user.Id.ToString()),
						new Claim("DisplayName", user.UserName),
						new Claim("UserName", user.UserName),
						new Claim("Email", user.EmailAddress)
					};

					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var token = new JwtSecurityToken(
						_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claims,
						expires: DateTime.UtcNow.AddMinutes(10),
						signingCredentials: signIn);

					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
				else
				{
					return BadRequest("Invalid credentials");
				}
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
