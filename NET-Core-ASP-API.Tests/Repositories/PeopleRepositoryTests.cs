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

            using (var repo = new PeopleRepository(_options))
            {
                repo.Database.EnsureDeleted();
                repo.CreatePerson(_person1);
                repo.CreatePerson(_person2);
                repo.CreatePerson(_person3);
            }
        }

        [Test]
        public void ShouldGetAllRecordsFromDb()
        {
            using (var repo = new PeopleRepository(_options))
            {
                var retrievedPeople = repo.GetPersons();
                retrievedPeople.Count().ShouldBe(3);
            }
        }

        [Test]
        public void ShouldGetSpecificRecordFromDb()
        {
            using (var repo = new PeopleRepository(_options))
            {
                var retrievedPerson = repo.GetPerson(2);
                retrievedPerson.Equals(_person2).ShouldBeTrue();
            }
        }

        [Test]
        public void ShouldCreateRecordInDb()
        {
            int id = 4;
            string firstName = "Rosie";
            string surname = "Butterworth";
            int age = 12;
            Person newPersonData = new Person { FirstName = firstName, Surname = surname, Age = age };
            Person expectedNewPerson = new Person { ID = id, FirstName = firstName, Surname = surname, Age = age };

            using (var repo = new PeopleRepository(_options))
            {
                repo.CreatePerson(newPersonData);
            }

            using (var repo = new PeopleRepository(_options))
            {
                repo.People.Count().ShouldBe(4);
                var createdPerson = repo.People.Single(q => q.ID == id);
                createdPerson.Equals(expectedNewPerson).ShouldBeTrue();
            }
        }

        [Test]
        public void ShouldUpdateRecordInDb()
        {
            int id = 1;
            string updateFirstName = "Rachel";
            string updateSurname = "Arms";
            int age = 20;
            Person updateData = new Person { ID = id, FirstName = updateFirstName, Surname = updateSurname, Age = age };
            Person expectedUpdatedPerson = new Person { ID = id, FirstName = updateFirstName, Surname = updateSurname, Age = age };

            using (var repo = new PeopleRepository(_options))
            {
                repo.UpdatePerson(updateData);
            }

            using (var repo = new PeopleRepository(_options))
            {
                var updatedPerson = repo.People.Single(q => q.ID == id);
                updatedPerson.Equals(expectedUpdatedPerson).ShouldBeTrue();
            }
        }

        [Test]
        public void ShouldDeleteRecordInDb()
        {
            int id = 1;

            using (var repo = new PeopleRepository(_options))
            {
                repo.DeletePerson(id);
            }

            using (var repo = new PeopleRepository(_options))
            {
                var deletedPerson = repo.People.SingleOrDefault(q => q.ID == id);
                deletedPerson.ShouldBeNull();
                repo.People.Count().ShouldBe(2);
            }
        }
    }
}
