using System;
namespace NETCoreASPAPI.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Person))
            {
                return false;
            }

            Person comparedPerson = obj as Person;
            return ID == comparedPerson.ID &&
                Age == comparedPerson.Age &&
                Equals(FirstName, comparedPerson.FirstName) &&
                Equals(Surname, comparedPerson.Surname);
        }
    }
}
