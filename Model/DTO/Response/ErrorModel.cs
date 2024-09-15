using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Response
{
    public class ErrorModel
    {
        public int Code {  get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
