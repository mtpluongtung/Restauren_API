using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Request.MonAn;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class MonAnRespository : IMonAnServices
    {
        private readonly RestaurentContext _context;
        public MonAnRespository(RestaurentContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<MonAnResponse>> Create(CreateMonAnRequest request)
        {
            var monAn = request.Adapt<MonAn>();
            _context.MonAns.Add(monAn);
            await _context.SaveChangesAsync();
            return new BaseResponse<MonAnResponse>().Success(monAn.Adapt<MonAnResponse>());
        }

        public async Task<BaseResponse<MonAnResponse>> Delete(long id)
        {
            var check = await _context.SetMonAns.AnyAsync(x => x.SetId == id);
            if (check)
            {
                throw new BaseException("Món ăn đã được sử dụng");
            }
            var monAn = await _context.MonAns.FindAsync(id);
            if (monAn == null)
            {
                throw new BaseException("Không tìm thấy món ăn");
            }
            _context.MonAns.Remove(monAn);
            await _context.SaveChangesAsync();
            return new BaseResponse<MonAnResponse>().Success(monAn.Adapt<MonAnResponse>());
        }

        public async Task<BaseResponse<List<MonAnResponse>>> GetAll()
        {
            var listMonAn = await _context.MonAns.ToListAsync();
            var result = listMonAn.Adapt<List<MonAnResponse>>();
            return new BaseResponse<List<MonAnResponse>>().Success(result);
        }

        public async Task<BaseResponse<MonAnResponse>> Update(UpdateMonAnRequest request)
        {
            var monAn = await _context.MonAns.FindAsync(request.Id);
            if (monAn == null) throw new BaseException("Không tìm thấy món ăn");

            monAn = request.Adapt<MonAn>();
            _context.MonAns.Update(monAn);
            await _context.SaveChangesAsync();
            return monAn.Adapt<BaseResponse<MonAnResponse>>();
        }
    }
}
