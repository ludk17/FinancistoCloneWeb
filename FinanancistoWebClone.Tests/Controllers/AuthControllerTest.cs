using FinancistoCloneWeb.Controllers;
using FinancistoCloneWeb.Models;
using FinancistoCloneWeb.Repositories;
using FinancistoCloneWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanancistoWebClone.Tests.Controllers
{
    public class AuthControllerTest
    {
        [Test]
        public void LoginRetornRedirecToAction()
        {                        
            var repositoryMock = new Mock<IUserRepository>();
            repositoryMock.Setup(o => o.FindUser("admin", "admin")).Returns(new User { });

            var authMock = new Mock<ICookieAuthService>();

            var controller = new AuthController(repositoryMock.Object, authMock.Object, null);
            var result = controller.Login("admin", "admin");

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
