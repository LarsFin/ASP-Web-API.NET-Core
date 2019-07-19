using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NETCoreASPAPI.Models;

namespace NETCoreASPAPI.Repositories
{
    public class PeopleRepository : DbContext, IPeopleRepository
    {
        public DbSet<Person> People { get; set; }

        private readonly static string _hostname = Environment.GetEnvironmentVariable("DB_HOSTNAME");
        private readonly static string _database = Environment.GetEnvironmentVariable("DB_NAME");
        private readonly static string _user = Environment.GetEnvironmentVariable("DB_USER");
        private readonly static string _password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={_hostname};Database={_database};Username={_user};Password={_password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<Person> GetPersons()
        {
            using (var db = new PeopleRepository())
            {
                var person = new Person { FirstName = "tom", Surname = "brad", Age = 30 };
                db.People.Add(person);
                db.SaveChanges();
            }

            return null;
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public Person CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Person UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }
    }
}
