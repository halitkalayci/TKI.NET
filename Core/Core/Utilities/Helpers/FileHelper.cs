using Core.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {
        public static string UploadFromBase64(string base64)
        {
            var base64Parts = base64.Split(",");
            if (base64Parts.Length != 2)
                throw new BusinessException("Geçerli bir base64 girilmedi.");

            byte[] fileBytes = Convert.FromBase64String(base64Parts[1]);

            var base64Header = base64Parts[0];
            var startIndex = base64Header.IndexOf("/");
            var endIndex = base64Header.Contains("+") ? base64Header.IndexOf("+") : base64Header.IndexOf(";");
            var extension = base64Header.Substring(startIndex + 1, endIndex - startIndex);

            // unique name
            // BASE64 içinden extension'ı okumak..
            var fileName = Guid.NewGuid().ToString() + "." + extension;
            File.WriteAllBytes(Environment.CurrentDirectory + @"\wwwroot\images\" + fileName, fileBytes);
            return fileName;
        }

        public static string UploadFromFile(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string extension = fileInfo.Extension;
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Environment.CurrentDirectory + @"\wwwroot\images\" + fileName;
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }
    }
}
