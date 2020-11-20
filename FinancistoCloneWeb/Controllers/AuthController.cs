using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FinancistoCloneWeb.Models;
using FinancistoCloneWeb.Repositories;
using FinancistoCloneWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinancistoCloneWeb.Controllers
{
    public class AuthController : Controller
    {

        public const String PASSWORD = "123";
        public string MiPropiedad { get; set; }

        private IConfiguration configuration;
        private ICookieAuthService cookieAuthService;
        private IUserRepository repository;

        public AuthController(IUserRepository repository, ICookieAuthService cookieAuthService, IConfiguration configuration)
        {     
            this.configuration = configuration;
            this.repository = repository;
            this.cookieAuthService = cookieAuthService;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            int target = -5;
            int num = 3;

            target = -num; 
            target = +num;


            if (repository.FindByUsername(user.Username) != null)
            {
                ModelState.AddModelError("Username", "Usuario Ya existe");
            }

            if (ModelState.IsValid)
            {
                user.Password = CreateHash(user.Password);
                repository.Create(user);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {         
            
                return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            /* validar si el usuario existe en la base de datos y 
             * verificar que el password sea correcto*/
            
            var user = repository.FindUser(username, CreateHash(password));

            if (user != null)
            {
                // Autenticaremos
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // cree la cookie y permita la autenticación
                cookieAuthService.SetHttpContext(HttpContext);
                cookieAuthService.Login(claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Login", "Usuario o contraseña incorrectos.");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
