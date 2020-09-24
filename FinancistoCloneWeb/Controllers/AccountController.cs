using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinancistoCloneWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FinancistoCloneWeb.Controllers
{
    public class AccountController : Controller
    {
        private FinancistoContext _context;
        public IHostEnvironment _hostEnv;

        public AccountController(FinancistoContext context, IHostEnvironment hostEnv)
        {
            _context = context;
            _hostEnv = hostEnv;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var account = new Account();            

            var accounts = _context.Accounts
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
            if(ModelState.IsValid)
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
        public ActionResult Edit(int id) // account/edit?id=10
        {
            ViewBag.Types = new List<string> { "Efectivo", "Debito", "Credito" };
            var account = _context.Accounts.Where(o => o.Id == id).FirstOrDefault(); // si no lo encutra retorna un null

            return View("Edit", account);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            //var dbAccount = _context.Accounts.Where(o => o.Id == account.Id).FirstOrDefault();
            //dbAccount.Name = account.Name;
            if (ModelState.IsValid)
            {
                _context.Accounts.Update(account);
                _context.SaveChanges();
                return RedirectToAction("Index"); 
            }
            ViewBag.Account = account;
            return View("edit");
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
