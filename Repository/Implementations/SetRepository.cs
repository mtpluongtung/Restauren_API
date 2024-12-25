using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Request.MonAn;
using Models.DTO.Request.Set;
using Models.DTO.Response;
using Repositories.Extension;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repositories.Implementations
{
	public class SetRepository : ISetServices
	{
		private readonly RestaurentContext _context;
		public SetRepository(RestaurentContext context)
		{
			_context = context;
		}
		public async Task<BaseResponse<SetResponse>> Create(CreateSetRequest request)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var set = request.Adapt<Set>();
				if (request.File != null && request.File.Length > 0)
				{
					set.Url = request.File.Upload();
				}

				await _context.Set.AddAsync(set);
				await _context.SaveChangesAsync(); // Lưu `set` để tạo `Id`

				var listSet = new List<SetMonAn>();
				foreach (var item in request.MonAn)
				{
					var setMonAn = new SetMonAn
					{
						MonAnId = item,
						SetId = set.Id // `set.Id` giờ đã được tạo
					};
					listSet.Add(setMonAn);
				}

				_context.SetMonAn.AddRange(listSet);
				await _context.SaveChangesAsync();

				await transaction.CommitAsync(); // Cam kết giao dịch
				return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync(); // Hủy giao dịch nếu lỗi
				throw;
			}
		}

		public async Task<BaseResponse<SetResponse>> Delete(long Id)
		{
			var set = await _context.Set.FindAsync(Id);
			if (set == null) throw new BaseException("Không tìm thấy món ăn");

			var check = await _context.SetMonAn.Where(x => x.SetId == Id).ToListAsync();

			_context.SetMonAn.RemoveRange(check);

			_context.Set.Remove(set);
			await _context.SaveChangesAsync();
			return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());
		}


		public async Task<PagedResult<SetResponse>> GetAll(BaseSearchRequest request)
		{
			var setWithMonAns = _context.Set
											  .Include(s => s.SetMonAn)  // Include bảng trung gian SetMonAn
												.ThenInclude(sma => sma.MonAn)  // Include bảng MonAn từ SetMonAn
												.Select(s => new SetResponse
												{
													Id = s.Id,
													Name = s.Name,
													Gia = s.Gia,
													Url = s.Url,
													MonAn = s.SetMonAn.Select(sma => new MonAnResponse
													{
														Id = sma.MonAn.Id,
														Name = sma.MonAn.Name  // Lấy thông tin từ MonAn
													}).ToList()
												})
												;
			var totalItems = await setWithMonAns.CountAsync();

			// 3. Lấy dữ liệu theo trang (PageNumber và PageSize)
			var items = await setWithMonAns.Where(x => string.IsNullOrEmpty(request.Text) || x.Name.Contains(request.Text))
				.Skip((request.Page - 1) * request.PageSize) // Bỏ qua các mục của các trang trước
				.Take(request.PageSize) // Lấy số lượng mục theo kích thước trang
				.ToListAsync();

			// 4. Chuyển đổi sang DTO (Data Transfer Object)
			var result = items.Adapt<List<SetResponse>>();

			// 5. Chuẩn bị dữ liệu phân trang
			var pagedResult = new PagedResult<SetResponse>
			{
				TotalRecords = totalItems,
				PageSize = request.PageSize,
				PageNumber = request.Page,
				Items = result
			};

			// 6. Trả về kết quả phân trang
			return pagedResult;
		}

		public async Task<SetResponse> GetById(long Id)
		{
			var setWithMonAns = await _context.Set.Where(x => x.Id == Id)
												.Include(s => s.SetMonAn)  // Include bảng trung gian SetMonAn
												.ThenInclude(sma => sma.MonAn)  // Include bảng MonAn từ SetMonAn
												.Select(s => new SetResponse
												{
													Id = s.Id,
													Name = s.Name,
													Gia = s.Gia,
													Url = s.Url,
													MonAn = s.SetMonAn.Select(sma => new MonAnResponse
													{
														Id = sma.MonAn.Id,
														Name = sma.MonAn.Name,
														Url = sma.MonAn.Url
													}).ToList()
												}).FirstOrDefaultAsync();

			if (setWithMonAns == null)
			{
				throw new BaseException("Không có set nào.");
			}

			return setWithMonAns;
		}

		public async Task<BaseResponse<SetResponse>> Update(UpdateSetRequest request)
		{
			var set = await _context.Set.FindAsync(request.Id);
			if (set == null) throw new BaseException("Không tìm thấy set này");

			set.Name = request.Name;
			set.Gia = request.Gia;
			set.Url = request.Url;

			_context.Set.Update(set);
			var setMonAn = await _context.SetMonAn.Where(x => x.SetId == request.Id).ToListAsync();
			_context.SetMonAn.RemoveRange(setMonAn);
			foreach (var item in request.MonAn)
			{
				var setMonNew = new SetMonAn();
				setMonNew.SetId = request.Id;
				setMonNew.MonAnId = item;

				_context.SetMonAn.Add(setMonNew);
			}
			await _context.SaveChangesAsync();
			return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());
		}
	}
}
