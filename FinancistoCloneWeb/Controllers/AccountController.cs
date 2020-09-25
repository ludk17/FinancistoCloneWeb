using FinancistoCloneWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace FinancistoCloneWeb.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private FinancistoContext _context;
        public IHostEnvironment _hostEnv;

        public AccountController(FinancistoContext context, IHostEnvironment hostEnv):base(context)
        {
            _context = context;
            _hostEnv = hostEnv;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var account = new Account();            
            var accounts = _context.Accounts
                .Where(o => o.UserId == LoggedUser().Id)
                .Include(o => o.Type)
                .ToList();

            return View(accounts);            
        }


        [HttpGet]
        public ActionResult Create() // GET
        {
            ViewBag.Types = _context.Types.ToList();
            return View("Create", new Account());
        }

        [HttpPost]
        public ActionResult Create(Account account, IFormFile image) // POST
        {
            account.UserId = LoggedUser().Id;

            if (ModelState.IsValid)
            {
                // guardar archivo en el servidor
                if(image != null && image.Length > 0)
                {
                    var basePath = _hostEnv.ContentRootPath + @"\wwwroot";
                    var ruta = @"\files\" + image.FileName;

                    using (var strem = new FileStream(basePath + ruta, FileMode.Create))
                    {
                        image.CopyTo(strem);
                        account.ImagePath = ruta;
                    }
                }
               
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Types = _context.Types.ToList();
            return View("Create", account);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Types = _context.Types.ToList();
            var account = _context.Accounts.Where(o => o.Id == id).FirstOrDefault(); // si no lo encutra retorna un null

            return View(account);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Accounts.Update(account);
                _context.SaveChanges();
                return RedirectToAction("Index"); 
            }

            ViewBag.Types = _context.Types.ToList();            
            return View(account);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var account = _context.Accounts.Where(o => o.Id == id).FirstOrDefault();
            _context.Accounts.Remove(account);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        

    }
}
