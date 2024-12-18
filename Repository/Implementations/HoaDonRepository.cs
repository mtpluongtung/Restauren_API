using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Request.HoaDon;
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
    public class HoaDonRepository : IHoaDonServices
    {
        private readonly RestaurentContext _context;
        public HoaDonRepository(RestaurentContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<HoaDonResponse>> Create(CreateHoaDonRequest reuquest)
        {
            try
            {
                var hoaDon = new HoaDon();
                hoaDon.MaOrder = reuquest.MaOrder;
                var tienMonAn = reuquest.MonAn.Select(x => x.ThanhTien).Sum();
                var tienSetMonAn = reuquest.MonAn.Select(x => x.ThanhTien).Sum();
                hoaDon.TongTien = tienMonAn + tienSetMonAn;
                var result = hoaDon.Adapt<HoaDonResponse>();
                foreach (var item in reuquest.Set)
                {
                    var setHoaDon = new HoaDonSetMonAn();
                    setHoaDon.SoLuong = item.SoLuong;
                    setHoaDon.MaOrder = reuquest.MaOrder;
                    setHoaDon.SetId = item.SetId;
                    setHoaDon.ThanhTien = item.ThanhTien;

                    await _context.HoaDonSetMonAn.AddAsync(setHoaDon);

                    result.SetMonAn.Add(setHoaDon.Adapt<SetInHoaDonResponse>());
                }

                foreach (var item in reuquest.MonAn)
                {
                    var monAn = new HoaDonMonAn();
                    monAn.SoLuong = item.SoLuong;
                    monAn.MaOrder = reuquest.MaOrder;
                    monAn.MonAnId = item.MonAnId;
                    monAn.ThanhTien = item.ThanhTien;

                    await _context.HoaDonMonAn.AddAsync(monAn);

                    result.MonAn.Add(monAn.Adapt<MonAnInHoaDonResponse>());
                }

                await _context.HoaDon.AddAsync(hoaDon);
                await _context.SaveChangesAsync();
                return new BaseResponse<HoaDonResponse>().Success(result);
            }
            catch (Exception ex) 
            {
                var message = ex.Message;
                throw ex;
            }
        }

        public async Task<BaseResponse<HoaDonResponse>> Delete(Guid Id)
        {
            var hoadon = await _context.HoaDon.Where(x=> x.MaOrder == Id).FirstOrDefaultAsync();
            if (hoadon == null) throw new BaseException("Hóa đơn không tồn tại");
            var hoadonMonAn = await _context.HoaDonMonAn.Where(x => x.MaOrder == Id).ToListAsync();
            var hoaDonSet = await _context.HoaDonSetMonAn.Where(x => x.MaOrder == Id).ToListAsync();

            _context.HoaDon.Remove(hoadon);
            _context.HoaDonMonAn.RemoveRange(hoadonMonAn);
            _context.HoaDonSetMonAn.RemoveRange(hoaDonSet);

            await _context.SaveChangesAsync();

            return new BaseResponse<HoaDonResponse>().Success(hoadon.Adapt<HoaDonResponse>());
        }

        public async Task<List<HoaDonResponse>> GetAll()
        {
            var hoaDon = await _context.HoaDon.ToListAsync();
            return hoaDon.Adapt<List<HoaDonResponse>>();
        }

        public async Task<BaseResponse<HoaDonResponse>> GetById(Guid Id)
        {
            var hoaDon = await _context.HoaDon
             .Where(hd => hd.MaOrder == Id)
             .Select(hd => new HoaDonResponse
             {
                 MaOrder = hd.MaOrder,
                 NgayTao = hd.NgayTao,
                 ThanhToan = hd.ThanhToan,
                 TongTien = _context.HoaDonSetMonAn
                     .Where(hdso => hdso.MaOrder == hd.MaOrder)
                     .Sum(hdso => hdso.SoLuong * hdso.ThanhTien) +
                 _context.HoaDonMonAn
                     .Where(hdma => hdma.MaOrder == hd.MaOrder)
                     .Sum(hdma => hdma.SoLuong * hdma.ThanhTien),
                 MonAn = new List<MonAnInHoaDonResponse>(),
                 SetMonAn = new List<SetInHoaDonResponse>()
             })
             .SingleOrDefaultAsync();

            if (hoaDon == null)
                return null;

            // Lấy danh sách các món ăn trong hóa đơn
            hoaDon.MonAn = await _context.HoaDonMonAn
                .Where(hdma => hdma.MaOrder == Id)
                .Select(hdma => new MonAnInHoaDonResponse
                {
                    MonAnId = hdma.MonAnId,
                    SoLuong = hdma.SoLuong,
                    ThanhTien = hdma.SoLuong * hdma.ThanhTien
                })
                .ToListAsync();

            // Lấy danh sách các set món ăn trong hóa đơn
            hoaDon.SetMonAn = await _context.HoaDonSetMonAn
                .Where(hdso => hdso.MaOrder == Id)
                .Select(hdso => new SetInHoaDonResponse
                {
                    SetId = hdso.SetId,
                    SoLuong = hdso.SoLuong,
                    ThanhTien = hdso.SoLuong * hdso.ThanhTien
                })
                .ToListAsync();

            return new BaseResponse<HoaDonResponse>().Success(hoaDon.Adapt<HoaDonResponse>()) ;
        }

        public Task<BaseResponse<HoaDonResponse>> Update(UpdateHoaDonRequest reuqest)
        {
            throw new NotImplementedException();
        }
    }
}
