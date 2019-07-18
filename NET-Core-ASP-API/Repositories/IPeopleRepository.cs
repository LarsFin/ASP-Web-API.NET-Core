using System;
using System.Collections.Generic;
using NETCoreASPAPI.Models;

namespace NETCoreASPAPI.Repositories
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> GetPersons();

        Person GetPerson(int id);

        Person CreatePerson(Person person);

        Person UpdatePerson(Person person);

        void DeletePerson(int id);
    }
}
