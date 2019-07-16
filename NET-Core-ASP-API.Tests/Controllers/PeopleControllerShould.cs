using System;
using System.Collections.Generic;
using Moq;
using NETCoreASPAPI.Controllers;
using NETCoreASPAPI.Models;
using NETCoreASPAPI.Services;
using NUnit.Framework;
using Shouldly;

namespace NETCoreASPAPI.Tests.Controllers
{
    public class PeopleControllerShould
    {
        private PeopleController controller;

        private Mock<IPersonService> personServiceMock;

        private static Person _bobPerson = new Person { ID = 1, FirstName = "Bob", Surname = "Roads", Age = 23 };

        private static IEnumerable<Person> _people = new List<Person> { _bobPerson };

        [SetUp]
        public void SetUp()
        {
            personServiceMock = new Mock<IPersonService>();
            controller = new PeopleController(personServiceMock.Object);
        }

        [Test]
        public void ReturnAllPeople()
        {
            personServiceMock.Setup(q => q.GetPersons()).Returns(_people);

            controller.Get().ShouldBe(_people);
        }
    }
}
