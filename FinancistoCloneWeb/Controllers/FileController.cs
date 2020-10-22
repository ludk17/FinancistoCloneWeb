using FinancistoCloneWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace FinancistoCloneWeb.Controllers
{
    public class FileController : Controller
    {
        private IFileService fileService;
        private readonly IHostEnvironment host;

        public FileController(IHostEnvironment host)
        {
            this.host = host;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(string dest, IFormFile file)
        {
            if (dest == "local")
                fileService = new LocalFileService(host);
            if (dest == "dropbox")
                fileService = new DropboxFileService();
            if (dest == "drive")
                fileService = new DriveFileService();


            fileService.Upload(file);
            return View();
        }
        
        [HttpPost]
        public IActionResult Donwload(string filename)
        {
            var file = fileService.Download(filename);
            return View();
        }
    }
}
