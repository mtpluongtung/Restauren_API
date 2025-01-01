using Repositories.Implementations;
using Repositories.Interfaces;
using Repository.Interfaces;

namespace Restaurant_API.Register
{
    public static class ConfigureServices
    {
        public static void AddServices(IServiceCollection services)
        {
            // Đăng ký service 
            services.AddScoped<IMonAnServices, MonAnRespository>();
            services.AddScoped<IBanServices, BanRespository>();
            services.AddScoped<IHoaDonServices, HoaDonRepository>();
            services.AddScoped<ISetServices, SetRepository>();
            services.AddScoped<INhanVienServices, NhanVienRepository>();
            services.AddScoped<IOrderSevices, OrderRepository>();
            services.AddScoped<IBepServices, BepRepository>();
            services.AddScoped<IAutheServices, AuthServicesRepository>();
            services.AddScoped<ICaLamViec, CaLamViecRepository>();

        }
    }
}
