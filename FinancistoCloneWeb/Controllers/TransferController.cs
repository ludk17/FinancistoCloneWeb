using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancistoCloneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancistoCloneWeb.Controllers
{
    public class TransferController : Controller
    {
        private readonly FinancistoContext context;

        public TransferController(FinancistoContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Accounts = context.Accounts.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(int sourceAccountId, int destAccountId, decimal ammount)
        {
            var transaction1 = new Transaction
            {
                AccountId = sourceAccountId,
                Date = DateTime.Now,
                Type = "GASTO",
                Summary = "Transferencia",
                Amount = ammount * -1
            };

            var transaction2 = new Transaction
            {
                AccountId = destAccountId,
                Date = DateTime.Now,
                Type = "INGRESO",
                Summary = "Transferencia",
                Amount = ammount
            };

            context.Transactions.Add(transaction1);
            context.Transactions.Add(transaction2);

            context.SaveChanges();

            UpdateAmountAcount(sourceAccountId);
            UpdateAmountAcount(destAccountId);

            return RedirectToAction("Index", "Account");
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
