using Entities.DTO.Response;
using Models.DTO.Request.MonAn;
using Models.DTO.Request.Set;
using Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISetServices
    {
        Task<BaseResponse<SetResponse>> Create(CreateSetRequest request);
        Task<BaseResponse<SetResponse>> Update(UpdateSetRequest request);
        Task<BaseResponse<SetResponse>> Delete(long Id);
        Task<PagedResult<SetResponse>> GetAll(SearchMonAnRequest request);
        Task<SetResponse> GetById(long Id);

    }
}
