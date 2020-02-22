using System;
using System.Collections.Generic;
using System.Linq;
using JsonParsingTest.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonParsingTest.Services
{
    public class PostgresPersonService : IPersonService
    {
        private readonly PersonContext _context;

        public PostgresPersonService(PersonContext context)
        {
            _context = context;
        }
        public Person Get(Guid personId)
        {
            return _context.Persons.Where(p => p.PersonId == personId).FirstOrDefault();
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons;
        }

        public Person Add(Person person)
        {
            Guid personGuid = Guid.NewGuid();
            person.PersonId = personGuid;
            _context.Persons.Add(person);

            _context.SaveChanges();
            return person;
        }

        #nullable enable
        public Person? Update(Guid personId, Person person)
        {

            person.PersonId = personId;
            _context.Entry(person).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!PersonExists(personId))
                {
                    return null;
                }
                else
                {
                    throw new Exception();
                }
            }

            return person;
        }

        #nullable enable
        public Person? Delete(Guid personId)
        {
            Person? requstedPerson = _context.Persons.Find(personId);
            #nullable disable

            if (requstedPerson == null)
            {
                return null;
            }

            _context.Persons.Remove(requstedPerson);
            _context.SaveChanges();
            return requstedPerson;
        }

        private bool PersonExists(Guid personId)
        {
            return _context.Persons.Any(person => personId == person.PersonId);
        }
    }
}