using Business;
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
            var hoaDon = new HoaDon();
            
            hoaDon.BanId = reuquest.BanId;
            hoaDon.MaHoaDon = Guid.NewGuid();
            var tienMonAn = reuquest.MonAn.Select(x => x.ThanhTien).Sum();
            var tienSetMonAn = reuquest.MonAn.Select(x => x.ThanhTien).Sum();
            hoaDon.TongTien = tienMonAn + tienSetMonAn;
            var result= hoaDon.Adapt<HoaDonResponse>();
            foreach (var item in reuquest.Set)
            {
                var setHoaDon = new HoaDonSetMonAn();
                setHoaDon.SoLuong = item.SoLuong;
                setHoaDon.HoaDonId = hoaDon.MaHoaDon;
                setHoaDon.SetId = item.SetId;
                setHoaDon.ThanhTien = item.ThanhTien;

                await _context.HoaDonSetMonAns.AddAsync(setHoaDon);

                result.SetMonAn.Add(setHoaDon.Adapt<SetInHoaDonResponse>());
            }

            foreach (var item in reuquest.MonAn)
            {
                var monAn = new HoaDonMonAn();
                monAn.SoLuong = item.SoLuong;
                monAn.HoaDonId = hoaDon.MaHoaDon;
                monAn.MonAnId = item.MonAnId;
                monAn.ThanhTien = item.ThanhTien;

                await _context.HoaDonMonAns.AddAsync(monAn);

                result.SetMonAn.Add(monAn.Adapt<SetInHoaDonResponse>());
            }
            await _context.SaveChangesAsync();
            return new BaseResponse<HoaDonResponse>().Success(result);
        }

        public Task<HoaDonResponse> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<HoaDonResponse> Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<HoaDonResponse> Update(UpdateHoaDonRequest reuqest)
        {
            throw new NotImplementedException();
        }
    }
}
