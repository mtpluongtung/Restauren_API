using Entities.DTO.Request.Ban;
using Entities.DTO.Response;
using Models.DTO.Request.Ban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBanServices
    {
        Task<BaseResponse<BanResponse>> Create(CreateBanRequest request);
        Task<BaseResponse<BanResponse>> Update(UpdateBanReuquest request);
        Task<BaseResponse<BanResponse>> Delete(long id);
        Task<BaseResponse<List<BanResponse>>> GetAll();
    }
}
