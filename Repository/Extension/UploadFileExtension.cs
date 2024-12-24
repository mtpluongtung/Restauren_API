using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extension
{
	public static class UploadFileExtension
	{
		public static string Upload(this IFormFile file)
		{
			var folderName = Path.Combine("Upload", "AnhMonAn");
			var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
			if (file.Length > 0)
			{
				var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
				var fullPath = Path.Combine(pathToSave, fileName);
				var dbPath = Path.Combine(folderName, fileName);
				using (var stream = new FileStream(fullPath, FileMode.Create))
				{
					file.CopyTo(stream);
				}
				return dbPath;
			}
			else
			{
				return null;
			}
		}
	}
}
