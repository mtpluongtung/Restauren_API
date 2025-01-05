using Business;
using Entities.DTO.ExceptinHandlering;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Constans;
using Models.DTO.Request.Bep;
using Models.DTO.Request.Order;
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
	public class OrderRepository : IOrderSevices
	{
		private readonly RestaurentContext _context;
		public OrderRepository(RestaurentContext context)
		{
			_context = context;
		}
		public async Task<OrderResponse> Create(CreateOrder createOrder)
		{
			try
			{
				var order = createOrder.Adapt<Order>();
				var ban = await _context.Ban.FindAsync(createOrder.BanId);
				if (ban != null)
				{
					ban.TrangThai = true;
				}

				await _context.Order.AddAsync(order);
				await _context.SaveChangesAsync();
				return order.Adapt<OrderResponse>();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<OrderResponse> GetByBanId(long id)
		{
			var result = await _context.Order.Where(x => x.BanId == id).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
			return result.Adapt<OrderResponse>();
		}

		public async Task<List<MenuResponse>> GetMenu()
		{
			var result = new List<MenuResponse>();
			var monAn = await _context.MonAn.Select(x => new MenuResponse
			{
				Id = x.Id,
				Name = x.Name,
				Url = x.Url,
				Gia = x.Gia,
				Type = x.Loai
			}
			).ToListAsync();
			var set = await _context.Set.Select(x => new MenuResponse
			{
				Id = x.Id,
				Name = x.Name,
				Url = x.Url,
				Gia = x.Gia,
				Type = TypeMenu.BUFFE
			}).ToListAsync();
			result.AddRange(monAn);
			result.AddRange(set);
			return result.OrderByDescending(x => x.Type).ToList();
		}

		public async Task<bool> KhachHangThemMon(BepCreateRequest request)
		{
			var order = await _context.Order.Where(x => x.MaOrder == request.MaOrder).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
			if (order == null) throw new BaseException("Mã Order không tồn tại . Vui lòng liên hệ nhân viên");

			var checkSet = await _context.HoaDonSetMonAn.AnyAsync(x=> x.MaOrder ==  request.MaOrder && x.SetId == request.SetId);
			if (!checkSet) throw new BaseException("Bạn không được order món ăn trong set này. Vui lòng chọn món trong set đã chọn !");

			var bep = request.MonAn.Adapt<List<Bep>>();
			bep.ForEach(x => x.BanId = order.BanId);
			await _context.Beps.AddRangeAsync(bep);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> KhachHangThemMonTinhTien(ThemMonTinhTien request)
		{
			var order = await _context.Order.Where(x => x.MaOrder == request.MaOrder).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
			if (order == null) throw new BaseException("Mã Order không tồn tại . Vui lòng liên hệ nhân viên");

			var hoadon = await _context.HoaDon.FirstOrDefaultAsync(x=> x.MaOrder == request.MaOrder);
			if(hoadon != null)
			{
				hoadon.TongTien += request.ThanhTien;
				_context.HoaDon.Update(hoadon);
			}
			var hoaDonMonAn = new HoaDonMonAn
			{
				MaOrder = request.MaOrder,
				MonAnId = request.IdMonAn,
				SoLuong = request.SoLuong,
				ThanhTien = request.ThanhTien
			};
			var bep = new Bep
			{
				IdMonAn = request.IdMonAn,
				SoLuong = request.SoLuong,
				BanId = order.BanId,
			};
			_context.HoaDonMonAn.Add(hoaDonMonAn);
		
			await _context.Beps.AddAsync(bep);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
