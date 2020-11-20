using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancistoCloneWeb.Models;
using FinancistoCloneWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinancistoCloneWeb.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository repository;
        private readonly FinancistoContext context;

        public PersonController(IPersonRepository repository, FinancistoContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Cities = context.Cities.ToList();
            var people = repository.GetAll();
            return View(people);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Cities = context.Cities.ToList();
            return View(new Person());
        }


        [HttpPost]
        public ActionResult Create(Person person)
        {                      
            if (ModelState.IsValid)
            {
                context.People.Add(person);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            Response.StatusCode = 400;
            ViewBag.Cities = context.Cities.ToList();
            return View(person);
        }

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var person = _context.People.Where(o => o.Id == id).FirstOrDefault();
        //    ViewBag.Cities = _context.Cities.ToList();
        //    return View(person);
        //}


        //[HttpPost]
        //public ActionResult Edit(Person person)
        //{
        //    // Validaciones
        //    var date = person.BirthDate;
        //    var now = DateTime.Now;
        //    var years = (now - date).TotalDays / 365.2425;
        //    if (years < 18 || years > 65)
        //        ModelState.AddModelError("BirthDate", "Edad no valida");


        //    if (ModelState.IsValid)
        //    {
        //        _context.People.Update(person);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Cities = _context.Cities.ToList();
        //    return View(person);
        //}

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    var person = _context.People.Where(o => o.Id == id).FirstOrDefault();
        //    _context.People.Remove(person);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
