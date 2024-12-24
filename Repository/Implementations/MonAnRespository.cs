using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Request.MonAn;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Request.MonAn;
using Models.DTO.Response;
using Repositories.Extension;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositories.Implementations
{
	public class MonAnRespository : IMonAnServices
	{
		private readonly RestaurentContext _context;
		public MonAnRespository(RestaurentContext context)
		{
			_context = context;
		}
		public async Task<BaseResponse<MonAnResponse>> Create(CreateOrUpdateMonAnRequest request)
		{
			if(request.Id == 0)
			{
				var monAn = request.Adapt<MonAn>();
				monAn.Url = request.File?.Upload();
				_context.MonAn.Add(monAn);
				await _context.SaveChangesAsync();
				return new BaseResponse<MonAnResponse>().Success(monAn.Adapt<MonAnResponse>());
			}
			else
			{
				var monAn = await _context.MonAn.FindAsync(request.Id);
				if (monAn == null) throw new BaseException("Không tìm thấy món ăn");
				_context.Entry(monAn).State = EntityState.Detached;
			
				monAn = request.Adapt<MonAn>();
				if (request.File != null && request.File.Length > 0)
				{
					monAn.Url = request.File.Upload();
				}
				_context.MonAn.Update(monAn);
				await _context.SaveChangesAsync();
				return monAn.Adapt<BaseResponse<MonAnResponse>>();
			}
			
		}

		public async Task<BaseResponse<MonAnResponse>> Delete(long id)
		{
			var check = await _context.SetMonAn.AnyAsync(x => x.SetId == id);
			if (check)
			{
				throw new BaseException("Món ăn đã được sử dụng");
			}
			var monAn = await _context.MonAn.FindAsync(id);
			if (monAn == null)
			{
				throw new BaseException("Không tìm thấy món ăn");
			}
			_context.MonAn.Remove(monAn);
			await _context.SaveChangesAsync();
			return new BaseResponse<MonAnResponse>().Success(monAn.Adapt<MonAnResponse>());
		}

		public async Task<BaseResponse<PagedResult<MonAnResponse>>> GetAll(SearchMonAnRequest request)
		{
			// 1. Lấy danh sách món ăn từ database
			var query = _context.MonAn.AsQueryable();

			// 2. Tính tổng số lượng phần tử (TotalItems)
			var totalItems = await query.CountAsync();

			// 3. Lấy dữ liệu theo trang (PageNumber và PageSize)
			var items = await query.Where(x=> string.IsNullOrEmpty(request.Text) || x.Name.Contains(request.Text))
				.Skip((request.Page - 1) * request.PageSize) // Bỏ qua các mục của các trang trước
				.Take(request.PageSize) // Lấy số lượng mục theo kích thước trang
				.ToListAsync();

			// 4. Chuyển đổi sang DTO (Data Transfer Object)
			var result = items.Adapt<List<MonAnResponse>>();

			// 5. Chuẩn bị dữ liệu phân trang
			var pagedResult = new PagedResult<MonAnResponse>
			{
				TotalRecords = totalItems,
				PageSize = request.PageSize,
				PageNumber = request.Page,
				Items = result
			};

			// 6. Trả về kết quả phân trang
			return new BaseResponse<PagedResult<MonAnResponse>>().Success(pagedResult);
		}

		public async Task<BaseResponse<MonAnResponse>> Update(UpdateMonAnRequest request)
		{
			var monAn = await _context.MonAn.FindAsync(request.Id);
			if (monAn == null) throw new BaseException("Không tìm thấy món ăn");

			monAn = request.Adapt<MonAn>();
			if(request.File != null)
			{
				monAn.Url = request.File.Upload();
			}
			_context.MonAn.Update(monAn);
			await _context.SaveChangesAsync();
			return monAn.Adapt<BaseResponse<MonAnResponse>>();
		}
	}
}
