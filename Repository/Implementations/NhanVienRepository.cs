using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DTO.Configs;
using Models.DTO.Request.NhanVien;
using Models.DTO.Response;
using Models.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{

    public class NhanVienRepository : INhanVienServices
    {
        private readonly RestaurentContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly BaseConfig _config;
        public NhanVienRepository(RestaurentContext context, IHttpContextAccessor contextAccessor, IOptions<BaseConfig> config)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _config = config.Value;
        }
        public async Task<bool> CheckIn(string token)
        {

            if (_config.Ip != _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()) throw new BaseException("Vui lòng truy cập từ địa chỉ nội bộ");

            var chamCong = new ChamCong();
            chamCong.CheckIn = DateTime.Now;
            chamCong.MaNhanVien = GetManhanvienFromToken(token);
            await _context.ChamCong.AddAsync(chamCong);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckOut(string token)
        {

            if (_config.Ip != _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()) throw new BaseException("Vui lòng truy cập từ địa chỉ nội bộ");

            var chamCong = new ChamCong();
            chamCong.CheckOut = DateTime.Now;
            chamCong.MaNhanVien = GetManhanvienFromToken(token);
            await _context.ChamCong.AddAsync(chamCong);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<NhanVienResponse> Create(CreateNhanVienRequest request)
        {
            var check = await _context.NhanVien.AnyAsync(x => x.MaNhanvien == request.MaNhanvien);
            if (check) throw new BaseException("Mã nhân viên đã tồn tại");

            var nhanvien = request.Adapt<NhanVien>();
            nhanvien.Token = GenerateToken(request.MaNhanvien);
            await _context.NhanVien.AddAsync(nhanvien);
            await _context.SaveChangesAsync();
            return nhanvien.Adapt<NhanVienResponse>();
        }

        public Task<string> GenCheckIn(string manhanvien)
        {
            var token = GenerateToken(manhanvien);
            return Task.FromResult(_config.CheckInUrl + token);
        }

        public Task<string> GenCheckOut(string manhanvien)
        {
            var token = GenerateToken(manhanvien);
            return Task.FromResult(_config.CheckOutUrl + token);
        }
        private string GenerateToken(string manhanvien)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("luong#duc#tung$dep%trai22ahihihihihahaha");

            // Tạo ClaimsIdentity và thêm manhanvien vào claims
            var claims = new ClaimsIdentity(new[]
            {
                new Claim("NhanVien", manhanvien)  // Thêm manhanvien vào claim
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddYears(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GetManhanvienFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("luong#duc#tung$dep%trai22ahihihihihahaha");

            // Cấu hình tham số để validate token
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Nếu token hết hạn thì sẽ không chấp nhận
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Loại bỏ thời gian chênh lệch mặc định (5 phút)
            };

            try
            {
                // Giải token và lấy các claim
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Lấy mã nhân viên từ claim
                var manhanvien = principal.Claims.FirstOrDefault(c => c.Type == "NhanVien")?.Value;

                return manhanvien; // Trả về mã nhân viên
            }
            catch
            {
                // Token không hợp lệ hoặc đã hết hạn
                return null;
            }
        }

    }
}
