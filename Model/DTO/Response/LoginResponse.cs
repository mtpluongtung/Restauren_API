using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public bool flag { get; set; } = false;
    }
}
