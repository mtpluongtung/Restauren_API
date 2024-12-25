using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Request.Ban;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Request.Ban;
using Models.DTO.Request.MonAn;
using Models.DTO.Response;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class BanRespository : IBanServices
    {
        private readonly RestaurentContext _context;
        public BanRespository(RestaurentContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<BanResponse>> Create(CreateBanRequest request)
        {
            var check = await _context.Ban.AnyAsync(x => x.TenBan == request.TenBan);
            if (check) throw new BaseException("Bàn đã tồn tại");

            var ban = request.Adapt<Ban>();
            await _context.Ban.AddAsync(ban);
            await _context.SaveChangesAsync();

            return new BaseResponse<BanResponse>().Success(ban.Adapt<BanResponse>());

        }

        public async Task<BaseResponse<BanResponse>> Delete(long id)
        {
            var ban = await _context.Ban.FindAsync(id);

            if (ban == null) throw new BaseException("Không tìm thấy bàn này");
            if (ban.TrangThai) throw new BaseException("Bàn này đang được sử dụng không thể xóa");

            _context.Ban.Remove(ban);
            await _context.SaveChangesAsync();
            return new BaseResponse<BanResponse>().Success(ban.Adapt<BanResponse>());
        }

        public async Task<BaseResponse<List<BanResponse>>> GetAll()
        {
            var listBan = await _context.Ban.AsNoTracking().ToListAsync();
            var result = listBan.Adapt<List<BanResponse>>();

            return new BaseResponse<List<BanResponse>>().Success(result);
        }

		public async Task<PagedResult<BanResponse>> GetTable(BaseSearchRequest request)
		{
			// 1. Lấy danh sách món ăn từ database
			var query = _context.Ban.AsQueryable();

			// 2. Tính tổng số lượng phần tử (TotalItems)
			var totalItems = await query.CountAsync();

			// 3. Lấy dữ liệu theo trang (PageNumber và PageSize)
			var items = await query.Where(x => string.IsNullOrEmpty(request.Text) || x.TenBan.Contains(request.Text))
				.Skip((request.Page - 1) * request.PageSize) // Bỏ qua các mục của các trang trước
				.Take(request.PageSize) // Lấy số lượng mục theo kích thước trang
				.ToListAsync();

			// 4. Chuyển đổi sang DTO (Data Transfer Object)
			var result = items.Adapt<List<BanResponse>>();

			// 5. Chuẩn bị dữ liệu phân trang
			var pagedResult = new PagedResult<BanResponse>
			{
				TotalRecords = totalItems,
				PageSize = request.PageSize,
				PageNumber = request.Page,
				Items = result
			};

			// 6. Trả về kết quả phân trang
			return pagedResult;
		}

		public async Task<BaseResponse<BanResponse>> Update(UpdateBanReuquest request)
        {
            var ban = await _context.Ban.FindAsync(request.Id);

            if (ban == null) throw new BaseException("Không tìm thấy bàn này");

            ban.TenBan = request.TenBan;
            ban.TrangThai = request.TrangThai;
            _context.Ban.Update(ban);

            await _context.SaveChangesAsync();
            return new BaseResponse<BanResponse>().Success(ban.Adapt<BanResponse>());
        }
    }
}
