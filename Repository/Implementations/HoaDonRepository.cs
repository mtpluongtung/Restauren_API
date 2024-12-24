using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Constans;
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
        public async Task<BaseResponse<HoaDonResponse>> Create(CreateHoaDonRequest request)
        {
            try
            {
                var order = await _context.Order.Where(x => x.MaOrder == request.MaOrder).FirstOrDefaultAsync();
                var checkOrder = await _context.HoaDon.Where(x => x.MaOrder == request.MaOrder).FirstOrDefaultAsync();
                if (checkOrder != null)
                {
                    var resultUpdate = await CapNhatOrder(request, checkOrder, order?.SoLuongKH ?? 1);
                    return resultUpdate;
                }
                else
                {
                    var hoaDon = new HoaDon();
                    hoaDon.MaOrder = request.MaOrder;
                    hoaDon.TongTien = request.TongTien;
                    var result = hoaDon.Adapt<HoaDonResponse>();
                    foreach (var item in request.Set)
                    {
                        var setHoaDon = new HoaDonSetMonAn();
                        setHoaDon.SoLuong = order?.SoLuongKH ?? 1;
                        setHoaDon.MaOrder = request.MaOrder;
                        setHoaDon.SetId = item.SetId;
                        setHoaDon.ThanhTien = item.ThanhTien;

                        await _context.HoaDonSetMonAn.AddAsync(setHoaDon);

                        result.SetMonAn.Add(setHoaDon.Adapt<SetInHoaDonResponse>());
                    }

                    foreach (var item in request.MonAn)
                    {
                        var monAn = new HoaDonMonAn();
                        monAn.SoLuong = item.SoLuong;
                        monAn.MaOrder = request.MaOrder;
                        monAn.MonAnId = item.MonAnId;
                        monAn.ThanhTien = item.ThanhTien;

                        await _context.HoaDonMonAn.AddAsync(monAn);

                        result.MonAn.Add(monAn.Adapt<MonAnInHoaDonResponse>());
                    }
                    await CreateBep(request);
                    await _context.HoaDon.AddAsync(hoaDon);
                    await _context.SaveChangesAsync();
                    return new BaseResponse<HoaDonResponse>().Success(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task CreateBep(CreateHoaDonRequest request)
        {
            var listSet = request.Set.Select(x => x.SetId).ToList();
            var monAnInSet = await _context.SetMonAn.Where(x => listSet.Contains(x.SetId)).Select(x => x.MonAnId).ToListAsync();
            var listBep = new List<Bep>();
            foreach (var item in request.MonAn)
            {
                var bep = new Bep();
                bep.IdMonAn = item.MonAnId;
                bep.BanId = request.BanId;
                bep.SoLuong = item.SoLuong;
                bep.TrangThai = TypeBep.DangCho;
                listBep.Add(bep);
            }
            foreach (var item in monAnInSet)
            {
                var bep = new Bep();
                bep.IdMonAn = item;
                bep.BanId = request.BanId;
                bep.SoLuong = 1;
                bep.TrangThai = TypeBep.DangCho;
                listBep.Add(bep);
            }
            await _context.Beps.AddRangeAsync(listBep);
            await _context.SaveChangesAsync();
        }

        private async Task<BaseResponse<HoaDonResponse>> CapNhatOrder(CreateHoaDonRequest request, HoaDon hoaDon, int slKhachHang)
        {

            var result = hoaDon.Adapt<HoaDonResponse>();
            foreach (var item in request.Set)
            {
                var checlSet = await _context.HoaDonSetMonAn.AnyAsync(x => x.MaOrder == request.MaOrder && x.SetId == item.SetId);
                if (!checlSet)
                {
                    var setHoaDon = new HoaDonSetMonAn();
                    setHoaDon.SoLuong = slKhachHang;
                    setHoaDon.MaOrder = request.MaOrder;
                    setHoaDon.SetId = item.SetId;
                    setHoaDon.ThanhTien += item.ThanhTien;
                    hoaDon.TongTien += item.ThanhTien;
                    await _context.HoaDonSetMonAn.AddAsync(setHoaDon);
                    result.SetMonAn.Add(setHoaDon.Adapt<SetInHoaDonResponse>());
                }


            }

            foreach (var item in request.MonAn)
            {
                var monAn = await _context.HoaDonMonAn.FirstOrDefaultAsync(x => x.MaOrder == request.MaOrder && x.MonAnId == item.MonAnId);
                if (monAn != null)
                {
                    monAn.SoLuong += item.SoLuong;
                    monAn.ThanhTien += item.ThanhTien;
                    _context.HoaDonMonAn.Update(monAn);

                    result.MonAn.Add(monAn.Adapt<MonAnInHoaDonResponse>());
                }
                else
                {
                    var monAnNew = new HoaDonMonAn();
                    monAnNew.SoLuong = item.SoLuong;
                    monAnNew.MaOrder = request.MaOrder;
                    monAnNew.MonAnId = item.MonAnId;
                    monAnNew.ThanhTien = item.ThanhTien;
                    await _context.HoaDonMonAn.AddAsync(monAnNew);

                    result.MonAn.Add(monAn.Adapt<MonAnInHoaDonResponse>());
                }
                hoaDon.TongTien += item.ThanhTien;
            }

            await CreateBep(request);
            _context.HoaDon.Update(hoaDon);
            await _context.SaveChangesAsync();
            return new BaseResponse<HoaDonResponse>().Success(result);
        }

        public async Task<BaseResponse<HoaDonResponse>> Delete(Guid Id)
        {
            var hoadon = await _context.HoaDon.Where(x => x.MaOrder == Id).FirstOrDefaultAsync();
            if (hoadon == null) throw new BaseException("Hóa đơn không tồn tại");
            var hoadonMonAn = await _context.HoaDonMonAn.Where(x => x.MaOrder == Id).ToListAsync();
            var hoaDonSet = await _context.HoaDonSetMonAn.Where(x => x.MaOrder == Id).ToListAsync();

            _context.HoaDon.Remove(hoadon);
            _context.HoaDonMonAn.RemoveRange(hoadonMonAn);
            _context.HoaDonSetMonAn.RemoveRange(hoaDonSet);

            await _context.SaveChangesAsync();

            return new BaseResponse<HoaDonResponse>().Success(hoadon.Adapt<HoaDonResponse>());
        }

        public async Task<PagedResult<HoaDonResponse>> GetAll(GetHoaDonRequest request)
        {
            // Xác định số lượng bản ghi trên mỗi trang và trang hiện tại
            int pageSize = request.PageSize > 0 ? request.PageSize : 25; // Mặc định là 10
            int pageNumber = request.Page > 0 ? request.Page : 1; // Mặc định là trang 1

            // Tính toán số lượng bản ghi cần bỏ qua
            int skip = (pageNumber - 1) * pageSize;


            var totalQuery = from hd in _context.HoaDon
                             join od in _context.Order on hd.MaOrder equals od.MaOrder

                             where (string.IsNullOrEmpty(request.Text) || od.TenKhachHang.Contains(request.Text) || od.Phone.Contains(request.Text))
                             && (!request.FromDate.HasValue || hd.NgayTao >= request.FromDate)
                             && (!request.ToDate.HasValue || hd.NgayTao <= request.ToDate)
                             select new HoaDonResponse
                             {
                                 Id = hd.Id,
                                 MaOrder = hd.MaOrder,
                                 NgayTao = hd.NgayTao,
                                 ThanhToan = hd.ThanhToan,
                                 TongTien = hd.TongTien,
                                 TenKhachHang = od.TenKhachHang,
                                 Phone = od.Phone
                             };
            // Áp dụng sắp xếp, phân trang
            var hoaDonPaged = await totalQuery
                .OrderByDescending(x => x.NgayTao)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            // Đếm tổng số lượng bản ghi
            int totalRecords = await totalQuery.CountAsync();

            // Chuẩn bị kết quả trả về dạng PagedResult
            var result = new PagedResult<HoaDonResponse>
            {
                Items = hoaDonPaged,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }

        public async Task<BaseResponse<HoaDonResponse>> GetById(Guid Id)
        {
            // Truy vấn hóa đơn
            var hoaDon = await _context.HoaDon
                .Where(hd => hd.MaOrder == Id)
                .Select(hd => new HoaDonResponse
                {
                    Id = hd.Id,
                    MaOrder = hd.MaOrder,
                    NgayTao = hd.NgayTao,
                    ThanhToan = hd.ThanhToan,
                    TongTien = hd.TongTien,
                    MonAn = new List<MonAnInHoaDonResponse>(), // Gán sau
                    SetMonAn = new List<SetInHoaDonResponse>() // Gán sau
                })
                .SingleOrDefaultAsync();

            if (hoaDon == null) throw new Exception("Hoa đơn notfound");

            // Truy vấn danh sách món ăn
            hoaDon.MonAn = await _context.HoaDonMonAn
                .Where(hdma => hdma.MaOrder == Id)
                .Join(_context.MonAn,
                      hdma => hdma.MonAnId,
                      ma => ma.Id,
                      (hdma, ma) => new MonAnInHoaDonResponse
                      {
                          MonAnId = hdma.MonAnId,
                          Name = ma.Name,
                          SoLuong = hdma.SoLuong,
                          ThanhTien = hdma.ThanhTien,
                          Gia = ma.Gia
                      })
                .ToListAsync();

            // Truy vấn danh sách set món ăn
            hoaDon.SetMonAn = await _context.HoaDonSetMonAn
                .Where(hdso => hdso.MaOrder == Id)
                .Join(_context.Set,
                      hdso => hdso.SetId,
                      set => set.Id,
                      (hdso, set) => new SetInHoaDonResponse
                      {
                          SetId = hdso.SetId,
                          SoLuong = hdso.SoLuong,
                          Name = set.Name,
                          ThanhTien = hdso.ThanhTien,
                          Gia = set.Gia
                      })
                .ToListAsync();
            var order = await _context.Order.Where(x => x.MaOrder == hoaDon.MaOrder).Join(
                _context.Ban,
                b => b.BanId,
                o => o.Id,
                (b, o) => new OrderInHaDonResponse
                {
                    BanId = b.BanId,
                    BanName = o.TenBan,
                    Phone = b.Phone,
                    TenKhachHang = b.TenKhachHang,
                    SoLuongKH = b.SoLuongKH

                }).FirstOrDefaultAsync();
            if (order != null)
            {
                hoaDon.OrderDetails = order.Adapt<OrderInHaDonResponse>();
            }

            if (hoaDon == null) return new BaseResponse<HoaDonResponse>();

            return new BaseResponse<HoaDonResponse>().Success(hoaDon.Adapt<HoaDonResponse>());
        }

        public Task<BaseResponse<HoaDonResponse>> Update(UpdateHoaDonRequest reuqest)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DoanhThuHoaDon>> DoanhThuHoaDon(GetByDateRequest request)
        {
            var dailyRevenue = await _context.HoaDon.Where(x => x.NgayTao >= request.From && x.NgayTao <= request.To && x.ThanhToan)
           .GroupBy(h => h.NgayTao.Date)
            .Select(g => new DoanhThuHoaDon
            {
                Ngay = g.Key.Date,
                Loai = "Doanh thu từ hóa đơn",
                DoanhThu = g.Sum(h => h.TongTien)
            }).ToListAsync();
            return dailyRevenue;
        }
        public async Task<List<DoanhThuChiTiet>> DoanhThuChiTiet(DateTime request)
        {
            // Lấy danh sách món ăn trong hóa đơn
            var monAnInHoaDon = await (
                from hd in _context.HoaDon
                join ma in _context.HoaDonMonAn on hd.MaOrder equals ma.MaOrder
                join monAn in _context.MonAn on ma.MonAnId equals monAn.Id
                where hd.NgayTao==request
                select new
                {
                    hd.MaOrder,
                    hd.TongTien,
                    ChiTietHoaDon = new ChiTietHoaDon
                    {
                        TenMonAn = monAn.Name,
                        Soluong = ma.SoLuong,
                        DonGia = monAn.Gia,
                        ThanhTien = ma.ThanhTien
                    }
                }
            ).ToListAsync();

            // Lấy danh sách set món ăn trong hóa đơn
            var setInHoaDon = await (
                from hd in _context.HoaDon
                join ma in _context.HoaDonSetMonAn on hd.MaOrder equals ma.MaOrder
                join set in _context.Set on ma.SetId equals set.Id
                join od in _context.Order on ma.MaOrder equals od.MaOrder
                where hd.NgayTao == request
                select new
                {
                    hd.MaOrder,
                    hd.TongTien,
                    ChiTietHoaDon = new ChiTietHoaDon
                    {
                        TenMonAn = set.Name,
                        Soluong = od.SoLuongKH,
                        DonGia = set.Gia,
                        ThanhTien = ma.ThanhTien
                    }
                }
            ).ToListAsync();

            // Gộp nhóm theo MaOrder và tạo danh sách DoanhThuChiTiet
            var doanhThuChiTietList = monAnInHoaDon
                .Concat(setInHoaDon)
                .GroupBy(x => new { x.MaOrder, x.TongTien })
                .Select(g => new DoanhThuChiTiet
                {
                    MaOrder = g.Key.MaOrder,
                    TongTien = g.Key.TongTien,
                    ChiTietHoaDon = g.Select(x => x.ChiTietHoaDon).ToList()
                })
                .ToList();
            return doanhThuChiTietList;
        }
        public async Task<DoanhThuChiTiet> HoaDonChiTiet(Guid mahoadon)
        {
            //var query = from hd in _contextHoaDon.Query()
            //			join dh in _contextDonHangChiTiet.Query()
            //				on hd.MaDonHang equals dh.MaDonHang into dhGroup // Use "into" to group the join results
            //			from dh in dhGroup.DefaultIfEmpty() // Left join by including dhGroup.DefaultIfEmpty()
            //			where hd.MaHoaDon == mahoadon
            //			group dh by new { hd.MaHoaDon, hd.TongTien } into g

            //			select new DoanhThuChiTiet
            //			{
            //				MaHoaDon = g.Key.MaHoaDon.ToString(),
            //				TongTien = g.Key.TongTien,
            //				ChiTietHoaDon = g.Where(dh => dh != null).Select(dh => new ChiTietHoaDon
            //				{
            //					TenMonAn = dh.Name, 
            //					Soluong = dh.SoLuong,
            //					DonGia = dh.Gia,
            //					ThanhTien = dh.Gia * dh.SoLuong
            //				}).ToList() // Lấy tất cả các chi tiết hóa đơn cho hóa đơn này
            //			};


            //return await query.FirstOrDefaultAsync();
            return null;

        }

        public async Task<bool> ThanhToan(long id)
        {
            var hoaDon = await _context.HoaDon.Where(x => x.Id == id && !x.ThanhToan).FirstOrDefaultAsync();
            if (hoaDon != null)
            {
                hoaDon.ThanhToan = true;
                await _context.SaveChangesAsync();
                return true;
            }
            throw new BaseException("Hoá đơn không tồn tại hoặc đã được thanh toán");
        }
    }
}
