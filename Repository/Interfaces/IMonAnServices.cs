using Entities.DTO.Request.MonAn;
using Entities.DTO.Response;
using Models.DTO.Request.MonAn;
using Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IMonAnServices
    {
        Task<BaseResponse<MonAnResponse>> Create(CreateOrUpdateMonAnRequest request);
        Task<BaseResponse<MonAnResponse>> Update(UpdateMonAnRequest request);
        Task<BaseResponse<MonAnResponse>> Delete(long id);
        Task<BaseResponse<PagedResult<MonAnResponse>>> GetAll(BaseSearchRequest request);
    }
}
