using System;
using System.Collections.Generic;
using NETCoreASPAPI.Models;

namespace NETCoreASPAPI.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> GetPersons();

        Person GetPerson(int id);

        int CreatePerson(Person person);

        Person UpdatePerson(int id, Person person);

        void DeletePerson(int id);
    }
}
