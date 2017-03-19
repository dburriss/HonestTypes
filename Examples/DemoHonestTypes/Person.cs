using HonestTypes.Contacts;
using System;

namespace DemoHonestTypes
{
    public class Person
    {
        public static Func<FirstNames, LastName, Email, Person>  Create = (type, country, number) 
            => new Person(type, country, number);

        public Person(FirstNames firstNames, LastName lastName, Email email)
        {
            FirstNames = firstNames;
            LastName = lastName;
            Email = email;
        }

        public Person()
        {}

        public FirstNames FirstNames { get; set; }
        public LastName LastName { get; set; }
        public Email Email { get; set; }
    }
}
