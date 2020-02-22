using System;
using System.Collections.Generic;
using System.Linq;
using JsonParsingTest.Models;

namespace JsonParsingTest.Services
{
    public class SimplePersonService : IPersonService
    {
        private static List<Person> PersonList = new List<Person>();
        public Person Get(Guid personId)
        {
            return PersonList.Where(p => p.PersonId == personId).FirstOrDefault();
        }

        public IEnumerable<Person> GetAll()
        {
            return PersonList;
        }

        public Person Add(Person person)
        {
            Guid personGuid = Guid.NewGuid();
            person.PersonId = personGuid;
            PersonList.Add(person);
            return person;
        }

        #nullable enable
        public Person? Update(Guid personId, Person person)
        {
            int location = PersonList.FindIndex(p => p.PersonId == personId);
            Person? requstedPerson = location == -1 ? null : PersonList[location];
            #nullable disable

            if (requstedPerson == null)
            {
                return null;
            }

            person.PersonId = personId;
            PersonList[location] = person;
            return person;
        }

        #nullable enable
        public Person? Delete(Guid personId)
        {
            int location = PersonList.FindIndex(p => p.PersonId == personId);
            Person? requstedPerson = location == -1 ? null : PersonList[location];
            #nullable disable

            if (requstedPerson == null)
            {
                return null;
            }

            PersonList.RemoveAt(location);
            return requstedPerson;
        }
    }
}