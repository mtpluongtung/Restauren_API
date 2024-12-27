
using Models.DTO.Request.Authe;
using Models.DTO.Response;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAutheServices
    {
        Task<LoginResponse> Login(LoginParam param );
		Task<LoginResponse> RefreshToken();
		Task<User> GetUserbyUsername(string username);

	}
}
