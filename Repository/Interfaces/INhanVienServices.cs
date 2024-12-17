using Models.DTO.Request.NhanVien;
using Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface INhanVienServices
    {
        Task<NhanVienResponse> Create(CreateNhanVienRequest request);
        Task<bool> CheckIn(string token);
        Task<bool> CheckOut(string token);
        Task<string> GenCheckOut(string manhanvien);
        Task<string> GenCheckIn(string manhanvien);
    }
}
