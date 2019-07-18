using System;
using System.Collections.Generic;
using Moq;
using NETCoreASPAPI.Models;
using NETCoreASPAPI.Repositories;
using NETCoreASPAPI.Services;
using NUnit.Framework;
using Shouldly;

namespace NETCoreASPAPI.Tests.Services
{
    public class PeopleServiceTests
    {
        private PeopleService service;

        private Mock<IPeopleRepository> peopleRepoMock;

        private static Person _bobPerson = new Person { ID = 1, FirstName = "Bob", Surname = "Roads", Age = 23 };

        private static IEnumerable<Person> _people = new List<Person> { _bobPerson };

        [SetUp]
        public virtual void SetUp()
        {
            peopleRepoMock = new Mock<IPeopleRepository>();
            service = new PeopleService(peopleRepoMock.Object);
        }

        [TestFixture]
        public class GetAllShould : PeopleServiceTests
        {
            [Test]
            public void ReturnAllPeople()
            {
                peopleRepoMock.Setup(q => q.GetPersons()).Returns(_people);

                service.GetPersons().ShouldBe(_people);
            }
        }

        [TestFixture]
        public class GetByIdShould : PeopleServiceTests
        {
            [Test]
            public void ReturnsPerson()
            {
                peopleRepoMock.Setup(q => q.GetPerson(1)).Returns(_bobPerson);

                var result = service.GetPerson(1);

                result.ShouldBeOfType<Person>();
                result.ShouldBe(_bobPerson);
            }

            [Test]
            public void ThrowsNotFound()
            {
                Should.Throw<KeyNotFoundException>(() => service.GetPerson(99)).Message.Equals("Could not find Person with ID '99'");
            }
        }

        [TestFixture]
        public class UpdateShould : PeopleServiceTests
        {
            private Person _updateData = new Person { FirstName = "Bob", Surname = "Streets", Age = 23 };

            [Test]
            public void CallUpdatePerson()
            {
                service.UpdatePerson(1, _updateData);
                peopleRepoMock.Verify(q => q.UpdatePerson(_updateData), Times.Once());
            }
        }
    }
}
