using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DTO.Configs;
using Models.DTO.Request.MonAn;
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

		private readonly IHttpContextAccessor _contextAccessor;
		private readonly BaseConfig _config;
		private readonly RestaurentContext _context;
		public NhanVienRepository(
			IOptions<BaseConfig> config, RestaurentContext context, IHttpContextAccessor httpContextAccessor)
		{
			_config = config.Value;
			_context = context;
			_contextAccessor = httpContextAccessor;
		}
		public async Task<bool> CheckIn(string token)
		{

			if (_config.Ip == _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()) throw new BaseException("Vui lòng truy cập từ địa chỉ nội bộ");


			var manhanvien = GetManhanvienFromToken(token);
			var date = DateTime.Now;
			var check = await _context.ChamCong.AnyAsync(x =>
			x.MaNhanVien == manhanvien &&
			x.CheckIn != null &&
			(x.CheckIn.Value.Day == date.Day && x.CheckIn.Value.Month == date.Month && x.CheckIn.Value.Year == date.Year)
			&& x.CheckOut == null);
			if (check) throw new BaseException("Bạn đã check in hôm nay rồi");

			var chamCong = new ChamCong();
			chamCong.CheckIn = date;
			chamCong.MaNhanVien = GetManhanvienFromToken(token);
			chamCong.TrangThai = false;
			_context.ChamCong.Add(chamCong);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> CheckOut(string token)
		{

			if (_config.Ip == _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()) throw new BaseException("Vui lòng truy cập từ địa chỉ nội bộ");
			var manhanvien = GetManhanvienFromToken(token);
			var date = DateTime.Now;
			var checkOut = await _context.ChamCong.Where(x =>
			x.MaNhanVien == manhanvien &&
			x.CheckIn != null &&
			(x.CheckIn.Value.Day == date.Day && x.CheckIn.Value.Month == date.Month && x.CheckIn.Value.Year == date.Year)
			&& x.CheckOut == null).FirstOrDefaultAsync();
			if (checkOut == null) throw new BaseException("Bạn đã chưa checkin");
			checkOut.CheckOut = date;
			await _context.SaveChangesAsync();
			return true;

		}

		public async Task<NhanVienResponse> CreateOrUpdate(UpdateNhanVienRequest request)
		{
			if (request.Id != 0) // Update logic
			{
				var nhanVien = await _context.NhanVien.FindAsync(request.Id);
				if (nhanVien == null) throw new BaseException("Nhân viên không tồn tại");
				var check = await _context.NhanVien.AnyAsync(x => x.MaNhanvien == request.MaNhanvien && request.Id != x.Id);
				if (check) throw new BaseException("Mã nhân viên đã tồn tại");

				// Cập nhật từng thuộc tính
				nhanVien.MaNhanvien = request.MaNhanvien;
				nhanVien.TenNhanvien = request.TenNhanvien;
				nhanVien.Phone = request.Phone;
				nhanVien.Address = request.Address;
				nhanVien.Email = request.Email;
				nhanVien.ViTri = request.ViTri;
				nhanVien.CapBac = request.CapBac;
				_context.NhanVien.Update(nhanVien);
			}
			else // Create logic
			{
				var check = await _context.NhanVien.AnyAsync(x => x.MaNhanvien == request.MaNhanvien);
				if (check) throw new BaseException("Mã nhân viên đã tồn tại");

				var nhanVien = request.Adapt<NhanVien>();
				nhanVien.Token = GenerateToken(request.MaNhanvien);
				_context.NhanVien.Add(nhanVien);
			}

			await _context.SaveChangesAsync();
			return request.Adapt<NhanVienResponse>();
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

		public async Task<PagedResult<ChamCongNhanVien>> GetChamCong(SearchChamCong request)
		{

			// 1. Lấy danh sách món ăn từ database
			var query = from cc in _context.ChamCong
						join nv in _context.NhanVien on cc.MaNhanVien equals nv.MaNhanvien
						where 
						(string.IsNullOrEmpty(request.Text) ||
						nv.TenNhanvien.Contains(request.Text) ||
						nv.MaNhanvien.Contains(request.Text)) && 
						(!request.From.HasValue || cc.CheckIn.Value.Date >= request.From.Value.Date) &&
						(!request.To.HasValue || cc.CheckIn.Value.Date <= request.To.Value.Date)
						select new ChamCongNhanVien
						{
							Id = cc.Id,
							MaNhanVien = cc.MaNhanVien,
							TenNhanVien = nv.TenNhanvien,
							CheckIn = cc.CheckIn,
							CheckOut = cc.CheckOut,
							TotalTime = cc.CheckOut != null ? (cc.CheckOut.Value - cc.CheckIn.Value).TotalHours : 0,
							TrangThai = cc.TrangThai
						};

			// 2. Tính tổng số lượng phần tử (TotalItems)
			var totalItems = await query.CountAsync();
			var result = await query.ToListAsync();
			var pagedResult = new PagedResult<ChamCongNhanVien>
			{
				TotalRecords = totalItems,
				PageSize = request.PageSize,
				PageNumber = request.Page,
				Items = result
			};
			return pagedResult;
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

		public async Task<PagedResult<NhanVienResponse>> GetNhanVien(BaseSearchRequest request)
		{
			// 1. Lấy danh sách món ăn từ database
			var query = _context.NhanVien.AsQueryable();

			// 2. Tính tổng số lượng phần tử (TotalItems)
			var totalItems = await query.CountAsync();

			// 3. Lấy dữ liệu theo trang (PageNumber và PageSize)
			var items = await query.Where(x => string.IsNullOrEmpty(request.Text) || x.TenNhanvien.Contains(request.Text) || x.MaNhanvien.Contains(request.Text))
				.Skip((request.Page - 1) * request.PageSize) // Bỏ qua các mục của các trang trước
				.Take(request.PageSize) // Lấy số lượng mục theo kích thước trang
				.ToListAsync();

			// 4. Chuyển đổi sang DTO (Data Transfer Object)
			var result = items.Adapt<List<NhanVienResponse>>();

			// 5. Chuẩn bị dữ liệu phân trang
			var pagedResult = new PagedResult<NhanVienResponse>
			{
				TotalRecords = totalItems,
				PageSize = request.PageSize,
				PageNumber = request.Page,
				Items = result
			};

			// 6. Trả về kết quả phân trang
			return pagedResult;
			
		}

		public async Task<bool> Delete(long Id)
		{
			var nhanvien = _context.NhanVien.FirstOrDefault(x => x.Id == Id);
			if (nhanvien == null) throw new BaseException("Không tìm thấy nhân viên");
			_context.NhanVien.Remove(nhanvien);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
