using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Services
{
    public class LocalFileService: IFileService
    {
        private readonly IHostEnvironment host;

        public LocalFileService(IHostEnvironment host)
        {
            this.host = host;
        }

        public object Download(object file)
        {
            throw new NotImplementedException();
        }

        public void Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var basePath = host.ContentRootPath + @"\wwwroot";
                var ruta = @"\files\" + file.FileName;

                using var strem = new FileStream(basePath + ruta, FileMode.Create);
                file.CopyTo(strem);
            }
        }
    }
}
