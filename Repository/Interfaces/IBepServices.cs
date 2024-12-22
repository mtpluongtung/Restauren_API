using Models.DTO.Request.Bep;
using Models.DTO.Response;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface IBepServices
	{
		Task Create(Bep bep);
		Task Update(UpdateBep request);
		Task<PagedResult<BepResponse>> GetAll(int page , int pageSize);
	}
}
