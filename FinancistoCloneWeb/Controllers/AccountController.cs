using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancistoCloneWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancistoCloneWeb.Controllers
{
    public class AccountController : Controller
    {
        private FinancistoContext _context;
        public AccountController(FinancistoContext context)
        {
            _context = context;

        }

        [HttpGet]
        public ActionResult Index() // GET y POST
        {
               var accounts = _context.Accounts.ToList();
            // ViewBag.Accounts = accounts; // forma A
               return View(accounts); // forma B 
            // Si no se envia el nombre de la vista, se usara una vista con el mismo nombre del metodo
            
            //return RedirectToAction("Edit", new { id = 1, nombre="Luis" }); //account/edit?id=1&nombre=Luis
        }


        [HttpGet]
        public ActionResult Create() // GET
        {
            return View("Create", new Account());
        }

        [HttpPost]
        public ActionResult Create(Account account) // POST
        {
            //if(account.Name == null || account.Name == "")
            //    ModelState.AddModelError("Name1", "El campo Nombre es requerido");

            if(ModelState.IsValid)
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

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
