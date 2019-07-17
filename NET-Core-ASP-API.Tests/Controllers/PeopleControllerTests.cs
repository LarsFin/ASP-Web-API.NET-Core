using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NETCoreASPAPI.Controllers;
using NETCoreASPAPI.Models;
using NETCoreASPAPI.Services;
using NUnit.Framework;
using Shouldly;

namespace NETCoreASPAPI.Tests.Controllers
{
    public class PeopleControllerTests
    {
        private PeopleController controller;

        private Mock<IPersonService> personServiceMock;

        private static Person _bobPerson = new Person { ID = 1, FirstName = "Bob", Surname = "Roads", Age = 23 };

        private static IEnumerable<Person> _people = new List<Person> { _bobPerson };

        [SetUp]
        public virtual void SetUp()
        {
            personServiceMock = new Mock<IPersonService>();
            controller = new PeopleController(personServiceMock.Object);
        }

        [TestFixture]
        public class GetAllShould : PeopleControllerTests
        {
            [Test]
            public void ReturnAllPeople()
            {
                personServiceMock.Setup(q => q.GetPersons()).Returns(_people);

                controller.Get().ShouldBe(_people);
            }
        }

        [TestFixture]
        public class GetByIdShould : PeopleControllerTests
        {
            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
                personServiceMock.Setup(q => q.GetPerson(1)).Returns(_bobPerson);
            }

            [Test]
            public void ReturnsOk()
            {
                var result = controller.Get(1);

                result.Result.ShouldBeOfType<OkObjectResult>();
            }

            [Test]
            public void ReturnsPerson()
            {
                var result = controller.Get(1);
                var okResult = result.Result as OkObjectResult;

                okResult.Value.ShouldBeOfType<Person>();
                okResult.Value.ShouldBe(_bobPerson);
            }

            [Test]
            public void ReturnsNotFound()
            {
                var result = controller.Get(99);

                result.Result.ShouldBeOfType<NotFoundResult>();
            }
        }

        [TestFixture]
        public class PutByIdShould : PeopleControllerTests
        {
            private Person _updateData = new Person { FirstName = "Bob", Surname = "Blyth", Age = 23 };

            private Person _updatedPerson = new Person { ID = 1, FirstName = "Bob", Surname = "Blyth", Age = 23 };

            [SetUp]
            public override void SetUp()
            {
                base.SetUp();

            }

            [Test]
            public void ReturnsOk()
            {
                var result = controller.Update(1, _updateData);

                result.Result.ShouldBeOfType<OkObjectResult>();
            }

            [Test]
            public void CallsUpdatePerson()
            {
                var result = controller.Update(1, _updateData);

                personServiceMock.Verify(q => q.UpdatePerson(_bobPerson), Times.Once());
            }

            [Test]
            public void ReturnUpdatedPerson()
            {
                personServiceMock.Setup(q => q.UpdatePerson(_updateData)).Returns(_updatedPerson);

                var result = controller.Update(1, _updateData);

                var okResult = result.Result as OkObjectResult;

                okResult.Value.ShouldBeOfType<Person>();
                okResult.Value.ShouldBe(_updatedPerson);
            }

            [Test]
            public void ReturnsNotFound()
            {
                var result = controller.Update(99, _updateData);

                result.Result.ShouldBeOfType<NotFoundResult>();
            }
        }
    }
}
