using System;
using System.Collections.Generic;
using Moq;
using NETCoreASPAPI.Lib;
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

        private static Person _person = new Person { ID = 1, FirstName = "Bob", Surname = "Roads", Age = 23 };

        private static IEnumerable<Person> _people = new List<Person> { _person };

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
                peopleRepoMock.Setup(q => q.GetPerson(1)).Returns(_person);

                var result = service.GetPerson(1);

                result.ShouldBeOfType<Person>();
                result.ShouldBe(_person);
            }

            [Test]
            public void ReturnsNull()
            {
                var result = service.GetPerson(99);

                result.ShouldBeNull();
            }
        }

        [TestFixture]
        public class CreateShould : PeopleServiceTests
        {
            private readonly static string _firstName = "Eve";
            private readonly static string _surname = "Harper";
            private readonly static int _age = 22;
            private readonly Person _personCreationData = new Person { FirstName = _firstName, Surname = _surname, Age = _age };
            private readonly Person _createdPerson = new Person { ID = 1, FirstName = _firstName, Surname = _surname, Age = _age };

            [Test]
            public void CallCreatePerson()
            {
                var result = service.CreatePerson(_personCreationData);

                peopleRepoMock.Verify(q => q.CreatePerson(_personCreationData), Times.Once());
            }

            [Test]
            public void ReturnCreatedPerson()
            {
                peopleRepoMock.Setup(q => q.CreatePerson(_personCreationData)).Returns(_createdPerson);

                var result = service.CreatePerson(_personCreationData);

                result.ShouldBeOfType<Person>();
                result.ShouldBe(_createdPerson);
            }

            [Test]
            public void ReturnConflict()
            {
                peopleRepoMock.Setup(q => q.CreatePerson(_personCreationData)).Throws<DuplicateRecordException>();

                Should.Throw<DuplicateRecordException>(() => service.CreatePerson(_personCreationData));
            }
        }

        [TestFixture]
        public class DeleteShould : PeopleServiceTests
        {
            [Test]
            public void CallsDeletePerson()
            {
                service.DeletePerson(1);

                peopleRepoMock.Verify(q => q.DeletePerson(1), Times.Once());
            }

            [Test]
            public void ThrowsNotFound()
            {
                Should.Throw<KeyNotFoundException>(() => service.DeletePerson(99)).Message.Equals("Could not find Person with ID '99'");
            }
        }
    }
}
