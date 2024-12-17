using Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DTO.Configs;
using Restaurant_API.Middleware;
using Restaurant_API.Register;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContextPool<RestaurentContext>(option =>
option.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(30))
);
// Thêm chính sách CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Địa chỉ frontend
              .AllowAnyHeader()                     // Cho phép mọi header
              .AllowAnyMethod();                    // Cho phép mọi phương thức (GET, POST, PUT, DELETE)
    });
});
ConfigureServices.AddServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<BaseConfig>(builder.Configuration.GetSection("BaseConfig"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
// Sử dụng CORS
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
