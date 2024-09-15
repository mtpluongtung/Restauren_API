using Repositories.Implementations;
using Repositories.Interfaces;

namespace Restaurant_API.Register
{
    public static class ConfigureServices
    {
        public static void AddServices(IServiceCollection services)
        {
            // Đăng ký service IMonAnServices và MonAnServices
            services.AddScoped<IMonAnServices, MonAnRespository>();
            services.AddScoped<IBanServices, BanRespository>();
        }
    }
}
