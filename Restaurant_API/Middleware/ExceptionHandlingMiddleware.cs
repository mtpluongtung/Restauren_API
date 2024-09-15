using Entities.DTO.ExceptinHandlering;
using Entities.DTO.Response;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Restaurant_API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            try
            {
                await _next(context);
            }
            catch (BaseException baseException)
            {
                // Xử lý các loại exception cụ thể kế thừa từ BaseException
                response.StatusCode = (int)HttpStatusCode.BadRequest; // Ví dụ: BadRequest cho BaseException
                var result = new ErrorModel
                {
                    Code = 400, // Mã lỗi tương ứng
                    Message = baseException.Message,
                    Status = false
                };

                var jsonResult = JsonSerializer.Serialize(result);
                await response.WriteAsync(jsonResult);
            }
            catch (Exception exception)
            {
                // Xử lý các exception khác
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = new ErrorModel
                {
                    Code = 500,
                    Message = exception.Message,
                    Status = false
                };

                var jsonResult = JsonSerializer.Serialize(result);
                await response.WriteAsync(jsonResult);
            }
        }
    }

}

