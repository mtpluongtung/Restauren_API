using Entities.DTO.Request.MonAn;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Request.Set
{
    public class CreateSetRequest
    {
        public string? Name { get; set; }
        public decimal? Gia { get; set; }
        public IFormFile? File { get; set; }
        public List<long> MonAn { get; set; } = new List<long>();

    }
}
