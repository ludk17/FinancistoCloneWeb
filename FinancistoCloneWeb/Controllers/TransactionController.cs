using System.Linq;
using FinancistoCloneWeb.Models;
using Microsoft.AspNetCore.Mvc;

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
            var transactions = context.Transactions.Where(o => o.AccountId == accountId).ToList();
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
            context.Transactions.Add(transaction);
            context.SaveChanges();
            // Actualizar saldo de la cuenta
            var account = context.Accounts.FirstOrDefault(o => o.Id == transaction.AccountId);
            if(transaction.Type == "GASTO")
                account.Amount -= transaction.Amount;
            else
                account.Amount += transaction.Amount;

            context.SaveChanges();

            return RedirectToAction("Index", new { accountId = transaction.AccountId });
        }
    }
}
