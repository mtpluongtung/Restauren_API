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
        private readonly IHttpContextAccessor _httpcontext;
        public AuthServicesRepository(RestaurentContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpcontext = httpContext;
        }
        public async Task<LoginResponse> Login(LoginParam param)
        {
            try
            {
                LoginResponse loginRespon = new LoginResponse();
                
                    loginRespon.Token = GenerateToken();
                    loginRespon.flag = true;
                    return loginRespon;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("luong#duc#tung$dep%trai22");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
       
    }
}
