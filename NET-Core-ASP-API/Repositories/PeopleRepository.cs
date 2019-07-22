using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETCoreASPAPI.Models;

namespace NETCoreASPAPI.Repositories
{
    public class PeopleRepository : DbContext, IPeopleRepository
    {
        public DbSet<Person> People { get; set; }

        public PeopleRepository(DbContextOptions<PeopleRepository> options) : base(options)
        { }

        public IEnumerable<Person> GetPersons()
        {
            return People;
        }

        public Person GetPerson(int id)
        {
            return People.SingleOrDefault(q => q.ID == id);
        }

        public Person CreatePerson(Person person)
        {
            People.Add(person);
            SaveChanges();
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            People.Update(person);
            SaveChanges();
            return person;
        }

        public void DeletePerson(int id)
        {
            var person = GetPerson(id);

            People.Remove(person);
            SaveChanges();
        }
    }
}
