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
        Task<BaseResponse<HoaDonResponse>> Update(UpdateHoaDonRequest reuqest);
        Task<BaseResponse<HoaDonResponse>> Delete(Guid Id);
        Task<BaseResponse<HoaDonResponse>> GetById(Guid Id);
    }
}
