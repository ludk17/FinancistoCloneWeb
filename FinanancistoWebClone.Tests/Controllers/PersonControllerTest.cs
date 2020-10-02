using FinancistoCloneWeb.Controllers;
using FinancistoCloneWeb.Models;
using FinancistoCloneWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanancistoWebClone.Tests.Controllers
{
    public class PersonControllerTest
    {
        [Test]
        public void IndexDebeRetornarUnaVista()
        {
            var repository = new Mock<IPersonRepository>();
            repository.Setup(o => o.GetAll()).Returns(new List<Person>());

            var controller = new PersonController(repository.Object);
            var index = controller.Index();
            Assert.IsInstanceOf<ViewResult>(index);
        }
    }
}
