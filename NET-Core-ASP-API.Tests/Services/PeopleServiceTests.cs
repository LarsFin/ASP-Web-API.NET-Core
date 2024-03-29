﻿using System;
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
            public void ThrowKeyNotFoundException()
            {
                Should.Throw<KeyNotFoundException>(() => service.GetPerson(99)).Message.ShouldBe("Could not find Person with ID '99'");
            }
        }

        [TestFixture]
        public class UpdateShould : PeopleServiceTests
        {
            private static int _id = 1;
            private static string _firstName = "Bob";
            private static string _updateSurname = "Streets";
            private static int _age = 23;

            private Person _updateData = new Person { FirstName = _firstName, Surname = _updateSurname, Age = _age };
            private Person _updatedPerson = new Person { ID = _id, FirstName = _firstName, Surname = _updateSurname, Age = _age };

            [SetUp]
            public override void SetUp()
            {
                base.SetUp();
                peopleRepoMock.Setup(q => q.GetPerson(_id)).Returns(_person);
                peopleRepoMock.Setup(q => q.UpdatePerson(_updatedPerson)).Returns(_updatedPerson);
            }

            [Test]
            public void CallUpdatePerson()
            {
                service.UpdatePerson(_id, _updateData);

                peopleRepoMock.Verify(q => q.UpdatePerson(_updatedPerson), Times.Once());
            }

            [Test]
            public void ReturnUpdatedPerson()
            {
                var result = service.UpdatePerson(_id, _updateData);

                result.ShouldBe(_updatedPerson);
            }

            [Test]
            public void ThrowKeyNotFoundException()
            {
                Should.Throw<KeyNotFoundException>(() =>
                {
                    service.UpdatePerson(99, _updateData);
                }).Message.ShouldBe("Could not find Person with ID '99'");
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
            public void ThrowDuplicateRecordException()
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
				peopleRepoMock.Setup(q => q.GetPerson(1)).Returns(_person);

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
