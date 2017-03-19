using HonestTypes.Contacts;
using LanguageExt;
using System;

namespace DemoHonestTypes
{
    using HonestTypes.Return;

    public interface IQueryPerson
    {
        Exceptional<Option<Person>> Get(Email email);
    }

    public class PersonRepository : IQueryPerson
    {
        public Exceptional<Option<Person>> Get(Email email)
        {
            try
            {
                Person person = QueryByEmail(email);
                Option<Person> result = person;
                return result;
            }
            catch (Exception ex)//only catch expected exceptions
            {
                return ex;
            }
        }

        private Person QueryByEmail(Email email)
        {
            return Person.Create("John", "Doe", email);
        }
    }
}
