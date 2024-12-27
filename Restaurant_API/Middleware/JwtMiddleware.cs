using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Appsettings;
using Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Restaurant_API.Middleware
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly JwtOptions _options;
		private readonly IServiceProvider _authe;

		public JwtMiddleware(RequestDelegate next, IOptions<JwtOptions> options, IServiceProvider authe)
		{
			_next = next;
			_options = options.Value;
			_authe = authe;
		}
		public async Task Invoke(HttpContext context)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

			if (token != null)
				attachUserToContext(context, token);

			await _next(context);
		}
		private async Task attachUserToContext(HttpContext context, string token)
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_options.Secret);
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);
				var jwtToken = (JwtSecurityToken)validatedToken;
				string userName = jwtToken.Claims.First(x => x.Type == "username").Value;
				using (var scope = _authe.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<IAutheServices>();
					context.Items["UserForSession"] = await db.GetUserbyUsername(userName);
				}

			}
			catch (Exception ex)
			{
				throw ex;//_logger.LogError(ex, ex.Message);
			}
		}
	}
}
