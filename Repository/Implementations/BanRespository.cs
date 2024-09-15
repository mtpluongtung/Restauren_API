using Business;
using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Request.Ban;
using Entities.DTO.Response;
using Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.DTO.Request.Ban;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class BanRespository : IBanServices
    {
        private readonly RestaurentContext _context;
        public BanRespository(RestaurentContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<BanResponse>> Create(CreateBanRequest request)
        {
            var check = await _context.Bans.AnyAsync(x => x.TenBan == request.TenBan);
            if (check) throw new BaseException("Bàn đã tồn tại");

            var ban = request.Adapt<Ban>();
            _context.Bans.Add(ban);
            await _context.SaveChangesAsync();

            return new BaseResponse<BanResponse>().Success(ban.Adapt<BanResponse>());

        }

        public async Task<BaseResponse<BanResponse>> Delete(long id)
        {
            var ban = await _context.Bans.FindAsync(id);

            if (ban == null) throw new BaseException("Không tìm thấy bàn này");
            if (ban.TrangThai) throw new BaseException("Bàn này đang được sử dụng không thể xóa");

            _context.Bans.Remove(ban);
            await _context.SaveChangesAsync();
            return new BaseResponse<BanResponse>().Success(ban.Adapt<BanResponse>());
        }

        public async Task<BaseResponse<List<BanResponse>>> GetAll()
        {
            var listBan = await _context.Bans.AsNoTracking().ToListAsync();
            var result = listBan.Adapt<List<BanResponse>>();

            return new BaseResponse<List<BanResponse>>().Success(result);
        }

        public async Task<BaseResponse<BanResponse>> Update(UpdateBanReuquest request)
        {
            var ban = await _context.Bans.FindAsync(request.Id);

            if (ban == null) throw new BaseException("Không tìm thấy bàn này");

            ban.TenBan = request.TenBan;
            ban.TrangThai = request.TrangThai;
            _context.Bans.Update(ban);

            await _context.SaveChangesAsync();
            return new BaseResponse<BanResponse>().Success(ban.Adapt<BanResponse>());
        }
    }
}
