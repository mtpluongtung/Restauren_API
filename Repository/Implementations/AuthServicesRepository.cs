using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DTO.Request.Authe;
using Models.DTO.Response;
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
                var user = _context.Users.FirstOrDefault(x => x.UserName == param.Username && x.Password == param.Password);

                if (user == null)
                {
                    return loginRespon;
                }
                else
                {
                    loginRespon.User = user;
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
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
       
    }
}
