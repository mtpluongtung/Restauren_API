﻿using Business;
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

            var checkBan = await _context.Bans.FindAsync(reuquest.BanId);
            if (checkBan == null) throw new BaseException("Bàn không tồn tại");
            if (checkBan.TrangThai) throw new BaseException("Bàn đang được sử dụng không thể tạo hóa đơn cho bàn này");


            var hoaDon = new HoaDon();
            hoaDon.BanId = reuquest.BanId;
            hoaDon.MaHoaDon = Guid.NewGuid();
            var tienMonAn = reuquest.MonAn.Select(x => x.ThanhTien).Sum();
            var tienSetMonAn = reuquest.MonAn.Select(x => x.ThanhTien).Sum();
            hoaDon.TongTien = tienMonAn + tienSetMonAn;
            var result = hoaDon.Adapt<HoaDonResponse>();
            foreach (var item in reuquest.Set)
            {
                var setHoaDon = new HoaDonSetMonAn();
                setHoaDon.SoLuong = item.SoLuong;
                setHoaDon.HoaDonId = hoaDon.MaHoaDon;
                setHoaDon.SetId = item.SetId;
                setHoaDon.ThanhTien = item.ThanhTien;

                await _context.HoaDonSetMonAn.AddAsync(setHoaDon);

                result.SetMonAn.Add(setHoaDon.Adapt<SetInHoaDonResponse>());
            }

            foreach (var item in reuquest.MonAn)
            {
                var monAn = new HoaDonMonAn();
                monAn.SoLuong = item.SoLuong;
                monAn.HoaDonId = hoaDon.MaHoaDon;
                monAn.MonAnId = item.MonAnId;
                monAn.ThanhTien = item.ThanhTien;

                await _context.HoaDonMonAn.AddAsync(monAn);

                result.SetMonAn.Add(monAn.Adapt<SetInHoaDonResponse>());
            }
            await _context.HoaDon.AddAsync(hoaDon);
            await _context.SaveChangesAsync();
            return new BaseResponse<HoaDonResponse>().Success(result);
        }

        public async Task<BaseResponse<HoaDonResponse>> Delete(Guid Id)
        {
            var hoadon = await _context.HoaDon.Where(x=> x.MaHoaDon == Id).FirstOrDefaultAsync();
            if (hoadon == null) throw new BaseException("Hóa đơn không tồn tại");
            var hoadonMonAn = await _context.HoaDonMonAn.Where(x => x.HoaDonId == Id).ToListAsync();
            var hoaDonSet = await _context.HoaDonSetMonAn.Where(x => x.HoaDonId == Id).ToListAsync();

            _context.HoaDon.Remove(hoadon);
            _context.HoaDonMonAn.RemoveRange(hoadonMonAn);
            _context.HoaDonSetMonAn.RemoveRange(hoaDonSet);

            await _context.SaveChangesAsync();

            return new BaseResponse<HoaDonResponse>().Success(hoadon.Adapt<HoaDonResponse>());
        }

        public Task<BaseResponse<HoaDonResponse>> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<HoaDonResponse>> Update(UpdateHoaDonRequest reuqest)
        {
            throw new NotImplementedException();
        }
    }
}
