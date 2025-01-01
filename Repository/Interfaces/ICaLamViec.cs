using Models.DTO.Request.CaLamViec;
using Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface ICaLamViec
	{
		Task<CaLamViecResponse> Create(CreateCaLamViec request);
		Task<CaLamViecResponse> Update(UpdateCaLamViecRequest request);
		Task<CaLamViecResponse> Delete(long id);
		Task<List<CaLamViecResponse>> GetAll();
		Task<CaLamViecResponse> GetById(long id);
	}
}
