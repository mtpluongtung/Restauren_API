using Business;
using Entities.DTO.ExceptinHandlering;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Request.CaLamViec;
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
	public class CaLamViecRepository : ICaLamViec
	{
		private readonly RestaurentContext _context;

		public CaLamViecRepository(RestaurentContext context)
		{
			_context = context;
		}
		public Task<CaLamViecResponse> Create(CreateCaLamViec request)
		{
			var caLamViec = new CaLamViec
			{
				GioBatDauCa = request.GioBatDauCa,
				GioKetThucCa = request.GioKetThucCa,
				LoaiCa = request.LoaiCa
			};
			_context.CaLamViecs.Add(caLamViec);
			_context.SaveChanges();
			return Task.FromResult(caLamViec.Adapt<CaLamViecResponse>());
		}

		public async Task<CaLamViecResponse> Delete(long id)
		{
			var caLamViec = await _context.CaLamViecs.FirstOrDefaultAsync(x => x.Id == id);
			if (caLamViec == null)
			{
				throw new BaseException("Vui Không tìm thấy ca này");
			}
			_context.CaLamViecs.Remove(caLamViec);
			await _context.SaveChangesAsync();
			return caLamViec.Adapt<CaLamViecResponse>();
		}

		public async Task<List<CaLamViecResponse>> GetAll()
		{
			var result = await _context.CaLamViecs.ToListAsync();
			return result.Adapt<List<CaLamViecResponse>>();
		}

		public Task<CaLamViecResponse> GetById(long id)
		{
			var caLamViec = _context.CaLamViecs.FirstOrDefault(x => x.Id == id);
			return Task.FromResult(caLamViec.Adapt<CaLamViecResponse>());
		}

		public async Task<CaLamViecResponse> Update(UpdateCaLamViecRequest request)
		{
			var caLamViec = await _context.CaLamViecs.FirstOrDefaultAsync(x => x.Id == request.Id);
			if (caLamViec == null)
			{
				throw new BaseException("Vui Không tìm thấy ca này");
			}
			caLamViec.GioBatDauCa = request.GioBatDauCa;
			caLamViec.GioKetThucCa = request.GioKetThucCa;
			caLamViec.LoaiCa = request.LoaiCa;
			await _context.SaveChangesAsync();
			return caLamViec.Adapt<CaLamViecResponse>();
		}
	}
}
