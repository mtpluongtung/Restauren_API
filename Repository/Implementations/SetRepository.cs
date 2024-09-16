using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Request.Set;
using Models.DTO.Response;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var set = new Set(request.Name, request.Gia, request.Url);
            await _context.Sets.AddAsync(set);
            foreach (var item in request.MonAn)
            {
                var setMonAn = new SetMonAn();
                setMonAn.MonAnId = item;
                setMonAn.SetId = set.Id;
                var check = await _context.SetMonAns.AnyAsync(x => x.MonAnId == item);
                if (check) continue;

                await _context.SetMonAns.AddAsync(setMonAn);
            }
            await _context.SaveChangesAsync();
            return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());
        }

        public async Task<BaseResponse<SetResponse>> Delete(long Id)
        {
            var set = await _context.Sets.FindAsync(Id);
            if (set == null) throw new BaseException("Không tìm thấy món ăn");

            var check = await _context.SetMonAns.AnyAsync(x => x.SetId == Id);
            if (check) throw new BaseException("Vui lòng xóa tất cả món ăn khỏi set");

            _context.Sets.Remove(set);
            await _context.SaveChangesAsync();
            return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());
        }


        public async Task<BaseResponse<List<SetResponse>>> GetAll()
        {
            var setWithMonAns = await _context.Sets
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
                                                .ToListAsync();

            if (setWithMonAns == null || !setWithMonAns.Any())
            {
                throw new BaseException("Không có set nào.");
            }

            return new BaseResponse<List<SetResponse>>().Success(setWithMonAns);
        }

        public async Task<BaseResponse<SetResponse>> GetById(long Id)
        {
            var setWithMonAns = await _context.Sets.Where(x => x.Id == Id)
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
                                                }).FirstOrDefaultAsync();

            if (setWithMonAns == null)
            {
                throw new BaseException("Không có set nào.");
            }

            return new BaseResponse<SetResponse>().Success(setWithMonAns);
        }

        public async Task<BaseResponse<SetResponse>> Update(UpdateSetRequest request)
        {
            var set = await _context.Sets.FindAsync(request.Id);
            if (set == null) throw new BaseException("Không tìm thấy set này");

            set.Name = request.Name;
            set.Gia = request.Gia;
            set.Url = request.Url;

            _context.Sets.Update(set);
            var setMonAn = await _context.SetMonAns.Where(x=> x.SetId ==  request.Id).ToListAsync();
             _context.SetMonAns.RemoveRange(setMonAn);
             foreach (var item in request.MonAn)
            {
                var setMonNew = new SetMonAn();
                setMonNew.SetId =request.Id;
                setMonNew.MonAnId = item;

                _context.SetMonAns.Adapt(setMonNew);
            }
            await _context.SaveChangesAsync();
            return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());
        }
    }
}
