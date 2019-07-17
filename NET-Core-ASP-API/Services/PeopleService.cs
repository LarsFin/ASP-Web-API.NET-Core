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

        public int CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int id)
        {
            return peopleRepository.GetPerson(id);
        }

        public IEnumerable<Person> GetPersons()
        {
            return peopleRepository.GetPersons();
        }

        public Person UpdatePerson(int id, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
