using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
	public class User
	{
		[Key]
		public long Id { get; set; }
		[Required]
		[MaxLength(150)]
		public string UserName { get; set; }
		[Required]
		[MaxLength(150)]
		public string Password { get; set; }
		[Required]
		[MaxLength(150)]
		public string Name { get; set; }
		public DateTime? LastLogin { get; set; }
		public int Level { get; set; }
	}
}
