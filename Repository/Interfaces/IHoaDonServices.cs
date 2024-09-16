using Entities.DTO.Response;
using Models.DTO.Request.HoaDon;
using Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IHoaDonServices
    {
        Task<BaseResponse<HoaDonResponse>> Create(CreateHoaDonRequest reuquest);
        Task<HoaDonResponse> Update(UpdateHoaDonRequest reuqest);
        Task<HoaDonResponse> Delete(Guid Id);
        Task<HoaDonResponse> Get(Guid Id);
    }
}
