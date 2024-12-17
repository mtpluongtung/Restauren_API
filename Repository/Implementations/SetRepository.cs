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
            await _context.Set.AddAsync(set);
            var listSet= new List<SetMonAn>();
            foreach (var item in request.MonAn)
            {
                var setMonAn = new SetMonAn
                {
                    MonAnId = item,
					SetId = set.Id
                };
                _context.Entry(setMonAn).State = EntityState.Detached;
                listSet.Add(setMonAn);
            }
            _context.SetMonAn.AddRange(listSet);
            await _context.SaveChangesAsync();
            return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());

        }

        public async Task<BaseResponse<SetResponse>> Delete(long Id)
        {
            var set = await _context.Set.FindAsync(Id);
            if (set == null) throw new BaseException("Không tìm thấy món ăn");

            var check = await _context.SetMonAn.AnyAsync(x => x.SetId == Id);
            if (check) throw new BaseException("Vui lòng xóa tất cả món ăn khỏi set");

            _context.Set.Remove(set);
            await _context.SaveChangesAsync();
            return new BaseResponse<SetResponse>().Success(set.Adapt<SetResponse>());
        }


        public async Task<BaseResponse<List<SetResponse>>> GetAll()
        {
            var setWithMonAns = await _context.Set
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
            var setMonAn = await _context.SetMonAn.Where(x=> x.SetId ==  request.Id).ToListAsync();
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
