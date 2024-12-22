using Business;
using Entities.DTO.ExceptinHandlering;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Constans;
using Models.DTO.Request.Bep;
using Models.DTO.Response;
using Models.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
	public class BepRepository : IBepServices
	{
		private readonly RestaurentContext _context;

		public BepRepository(RestaurentContext context)
		{
			_context = context;

		}
		public async Task Create(Bep bep)
		{
			_context.Beps.Add(bep);
			await _context.SaveChangesAsync();
		}

		public async Task<PagedResult<BepResponse>> GetAll(int page, int pageSize)
		{
			var query = from bep in _context.Beps
						join ban in _context.Ban on bep.BanId equals ban.Id
						join mon in _context.MonAn on bep.IdMonAn equals mon.Id
						select new BepResponse
						{
							Id = bep.Id,
							Name = mon.Name,
							TenBan = ban.TenBan,
							SoLuong = bep.SoLuong,
							TrangThai = bep.TrangThai,
							CreatedDate = bep.CreatedDate,
							SuccesTime = bep.SuccesTime
						};
			var item = await query.OrderBy(x => x.TrangThai).ThenBy(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			int totalRecords = await query.CountAsync();

			// Chuẩn bị kết quả trả về dạng PagedResult
			var result = new PagedResult<BepResponse>
			{
				Items = item.Adapt<List<BepResponse>>(),
				TotalRecords = totalRecords,
				PageNumber = page,
				PageSize = pageSize
			};

			return result;
		}

		public async Task Update(UpdateBep request)
		{
			var bep = await _context.Beps.FindAsync(request.Id);
			if (bep == null)
			{
				throw new BaseException("Bep not found");
			}
			if(bep.SoLuong < request.SoLuong)
			{
				throw new BaseException("Cập nhật quá số lượng cho phép");
			}
			if (request.SoLuong == bep.SoLuong)
			{
				bep.TrangThai = request.TrangThai;
				if (request.TrangThai == TypeBep.DaXong)
				{
					bep.SuccesTime = DateTime.Now;
				}
			}
			else
			{
				var bepNew = new Bep
				{
					IdMonAn = bep.IdMonAn,
					BanId = bep.BanId,
					SoLuong = bep.SoLuong - request.SoLuong,
					TrangThai = bep.TrangThai,
					CreatedDate = bep.CreatedDate
				};
				bep.SoLuong = request.SoLuong;
				bep.TrangThai=request.TrangThai;
				if (request.TrangThai == TypeBep.DaXong)
				{
					bep.SuccesTime = DateTime.Now;
				}
				_context.Beps.Add(bepNew);
			}
			_context.Beps.Update(bep);
			await _context.SaveChangesAsync();

		}
	}
}
