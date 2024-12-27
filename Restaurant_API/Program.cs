using Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Models.DTO.Configs;
using Restaurant_API;
using Restaurant_API.Middleware;
using Restaurant_API.Register;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContextPool<RestaurentContext>(option =>
option.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(30))
);
// Thêm chính sách CORS
var requiredOrigins = builder.Configuration.GetSection("RequestOrigin").Get<string>();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigins", policy =>
	{
		policy.WithOrigins("http://gunsan.vn") // Địa chỉ frontend
			 .AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials(); // Cho phép gửi cookie/credentials                 // Cho phép mọi phương thức (GET, POST, PUT, DELETE)
	});
});
builder.Services.AddSignalR();
ConfigureServices.AddServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<BaseConfig>(builder.Configuration.GetSection("BaseConfig"));
var app = builder.Build();

app.UseCors("AllowSpecificOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<HubContext>("/HubContext");
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(Directory.GetCurrentDirectory(), "Upload")),
	RequestPath = "/Upload"
});
app.Run();
