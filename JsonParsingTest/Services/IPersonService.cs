using System;
using System.Collections.Generic;
using JsonParsingTest.Models;

namespace JsonParsingTest.Services
{
    public interface IPersonService
    {
        Person Get(Guid personId);
        IEnumerable<Person> GetAll();
        Person Add(Person person);

        #nullable enable
        Person? Update(Guid personId, Person person);
        #nullable disable

        #nullable enable
        Person? Delete(Guid personId);
        #nullable disable
    }
}