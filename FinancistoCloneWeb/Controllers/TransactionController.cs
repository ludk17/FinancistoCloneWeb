using System;
using System.Collections.Generic;
using System.Linq;
using FinancistoCloneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancistoCloneWeb.Controllers
{
    public class TransactionController : Controller
    {
        private readonly FinancistoContext context;

        public TransactionController(FinancistoContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(int accountId)
        {
            var transactions = context.Transactions
                .Where(o => o.AccountId == accountId)
                .OrderByDescending(o => o.Date)
                .ToList();
            ViewBag.Account = context.Accounts.First(o => o.Id == accountId);
            return View(transactions);
        }

        [HttpGet]
        public IActionResult Create(int accountId)
        {
            ViewBag.AccountId = accountId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AccountId = transaction.AccountId;
                return View(transaction);
            }

            // guardar transacción
            if (transaction.Type == "GASTO")
                transaction.Amount *= -1;            
            context.Transactions.Add(transaction);
            context.SaveChanges();
            // Actualizar saldo de la cuenta
            UpdateAmountAcount(transaction.AccountId);

            return RedirectToAction("Index", new { accountId = transaction.AccountId });
        }        

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(o => o.Id == id);
            return View(transaction);
        }

        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            if (!ModelState.IsValid)
                return View(transaction);

            // guardar transacción
            if (transaction.Type == "GASTO")
                transaction.Amount *= -1;
            context.Transactions.Update(transaction);
            context.SaveChanges();
            // Actualizar saldo de la cuenta
            UpdateAmountAcount(transaction.AccountId);

            return RedirectToAction("Index", new { accountId = transaction.AccountId });
        }

        private void UpdateAmountAcount(int accountId)
        {
            var account = context.Accounts
                .Include(o => o.Transactions)
                .FirstOrDefault(o => o.Id == accountId);

            var total = account.Transactions.Sum(o => o.Amount);
            account.Amount = total;
            context.SaveChanges();
        }
    }
}
