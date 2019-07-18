using System;
using System.Collections.Generic;
using NETCoreASPAPI.Models;
using NETCoreASPAPI.Repositories;

namespace NETCoreASPAPI.Services
{
    public class PeopleService : IPersonService
    {
        private IPeopleRepository peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            this.peopleRepository = peopleRepository;
        }

        public Person CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int id)
        {
            var person = peopleRepository.GetPerson(id);

            if (person == null)
            {
                throw new KeyNotFoundException($"Could not find Person with ID '{id}'");
            }

            return person;
        }

        public IEnumerable<Person> GetPersons()
        {
            return peopleRepository.GetPersons();
        }

        public Person UpdatePerson(int id, Person person)
        {
            person.ID = id;
            var updatedPerson = peopleRepository.UpdatePerson(person);
            return updatedPerson;
        }
    }
}
