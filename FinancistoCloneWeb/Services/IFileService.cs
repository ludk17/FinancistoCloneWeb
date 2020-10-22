using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Services
{
    public interface IFileService
    {
        void Upload(IFormFile file);
        object Download(object file);
    }
}
