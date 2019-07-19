using Microsoft.EntityFrameworkCore;
using NETCoreASPAPI.Models;
using NETCoreASPAPI.Repositories;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;

namespace NETCoreASPAPI.Tests.Repositories
{
    public class PeopleRepositoryTests
    {
        private DbContextOptions<PeopleRepository> _options;

        // Demo people for inMem db
        private Person _person1 = new Person { FirstName = "Rachel", Surname = "Snow", Age = 20 };
        private Person _person2 = new Person { FirstName = "Arnie", Surname = "Bull", Age = 31 };
        private Person _person3 = new Person { FirstName = "Phelan", Surname = "Fudge", Age = 63 };

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<PeopleRepository>()
                .UseInMemoryDatabase(databaseName: "test_db")
                .Options;
        }

        [Test]
        public void ShouldGetAllRecordsFromDb()
        {
            using (var repo = new PeopleRepository(_options))
            {
                repo.CreatePerson(_person1);
                repo.CreatePerson(_person2);
                repo.CreatePerson(_person3);
            }

            using (var repo = new PeopleRepository(_options))
            {
               
            }
        }

        [Test]
        public void ShouldGetSpecificRecordFromDb()
        {

        }

        [Test]
        public void ShouldCreateRecordInDb()
        {
            using (var repo = new PeopleRepository(_options))
            {
                repo.CreatePerson(_person1);
            }

            using (var repo = new PeopleRepository(_options))
            {
                repo.People.Count().ShouldBe(1);
                var createdPerson = repo.People.Single();
                createdPerson.Equals(_person1).ShouldBeTrue();
            }
        }

        [Test]
        public void ShouldUpdateRecordInDb()
        {

        }

        [Test]
        public void ShouldDeleteRecordInDb()
        {
            Person person = new Person { FirstName = "Rachel", Surname = "Snow", Age = 20 };

            using (var repo = new PeopleRepository(_options))
            {
                repo.CreatePerson(person);
                repo.DeletePerson(1);
            }

            using (var repo = new PeopleRepository(_options))
            {
                repo.People.ShouldBeEmpty();
            }
        }
    }
}
