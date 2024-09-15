using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Response
{
    public class BaseResponse<T> where T : class
    {
        public int Code { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public BaseResponse<T> Success(T data)
        {
            return new BaseResponse<T>
            {
                Code = 200,
                Message = "Thành công",
                Status = true,
                Data = data,

            };
        }
        public BaseResponse<T> Error(T data)
        {
            return new BaseResponse<T>
            {
                Code = 400,
                Message = "Thất bại",
                Status = false
            };
        }
    }
}
