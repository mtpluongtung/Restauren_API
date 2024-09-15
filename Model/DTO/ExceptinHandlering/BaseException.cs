using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.ExceptinHandlering
{
    public class BaseException : Exception
    {
        // Thêm thuộc tính tùy chỉnh nếu cần
        public int ErrorCode { get; set; }

        // Constructor không tham số
        public BaseException() : base()
        {
        }

        // Constructor nhận message
        public BaseException(string message) : base(message)
        {
        }

        // Constructor nhận message và innerException
        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // Constructor nhận message và errorCode
        public BaseException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
