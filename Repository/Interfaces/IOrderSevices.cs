using Models.DTO.Request.Bep;
using Models.DTO.Request.Order;
using Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderSevices
    {
        Task<OrderResponse> Create(CreateOrder createOrder);
        Task<List<MenuResponse>> GetMenu();
        Task<OrderResponse> GetByBanId(long id);
        Task<bool> KhachHangThemMon(BepCreateRequest request);
        Task<bool> KhachHangThemMonTinhTien(ThemMonTinhTien request);

	}
    
}
