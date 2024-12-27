using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Appsettings;
using Models.DTO.Request.Authe;
using Models.DTO.Response;
using Models.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
	public class AuthServicesRepository : IAutheServices
	{
		private readonly RestaurentContext _context;
		private readonly JwtOptions _jwtOptions;
		private readonly IHttpContextAccessor _httpcontext;
		public AuthServicesRepository(RestaurentContext context, IOptions<JwtOptions> jwtOptions, IHttpContextAccessor httpContext)
		{
			_context = context;
			_jwtOptions = jwtOptions.Value;
			_httpcontext = httpContext;
		}
		public async Task<LoginResponse> Login(LoginParam param)
		{
			try
			{
				LoginResponse loginRespon = new LoginResponse();
				var user = _context.Users.FirstOrDefault(x => x.UserName == param.userName && x.Password == param.passWord);

				if (user == null)
				{
					return loginRespon;
				}
				else
				{
					loginRespon.UserName = user.UserName;
					loginRespon.Token = GenerateToken(user);
					loginRespon.flag = true;
					return loginRespon;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private string GenerateToken(User applicationUser)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("username", applicationUser.UserName) }),
				Expires = DateTime.UtcNow.AddMinutes(35),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
		public async Task<LoginResponse> RefreshToken()
		{
			LoginResponse UserRespon = new LoginResponse();
			var user = (User)_httpcontext.HttpContext.Items["UserForSession"];
			if (user == null)
			{
				return null;
			}
			else
			{
				UserRespon.UserName = user.UserName;
				UserRespon.Token = GenerateToken(user);
				UserRespon.flag = true;
				return UserRespon;
			}
		}
		public async Task<User> GetUserbyUsername(string username)
		{
			try
			{
				var user = _context.Users.FirstOrDefault(x => x.UserName == username);
				return user;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
