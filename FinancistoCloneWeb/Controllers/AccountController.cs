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
            ViewBag.Accounts = _context.Accounts.ToList();
            return View("Index");
        }


        [HttpGet]
        public ActionResult Create() // GET
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Account account) // POST
        {
            //if(account.Name == null || account.Name == "")
            //    ModelState.AddModelError("Name1", "El campo Nombre es requerido");

            if(ModelState.IsValid)
            {
                _context.Accounts.Add(account);
                //_context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create");
            //             
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Types = new List<string> { "Efectivo", "Debito", "Credito" };
            ViewBag.Account = _context.Accounts.Where(o => o.Id == id).FirstOrDefault(); // si no lo encutra retorna un null

            return View("Edit");
        }

    }
}
