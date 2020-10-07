
using FinancistoCloneWeb.Controllers;
using FinancistoCloneWeb.Models;
using FinancistoCloneWeb.Repositories;
using FinancistoCloneWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            repositoryMock.Setup(o => o.FindUser("admin", It.IsAny<String>())).Returns(new User { });

            var authMock = new Mock<ICookieAuthService>();

            var congifMock = new Mock<IConfiguration>();
            congifMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);

            var controller = new AuthController(repositoryMock.Object, authMock.Object, congifMock.Object);
            var result = controller.Login("admin", "admin");

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
