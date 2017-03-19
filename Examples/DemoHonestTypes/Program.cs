using HonestTypes.Contacts;
using HonestTypes.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoHonestTypes
{
    using LanguageExt;
    using static LanguageExt.Prelude;
    class Program
    {
        static void Main(string[] args)
        {
            Func<Person, Contact> toContact = p => new Contact
            {
                Name = $"{p.FirstNames} {p.LastName}",
                Email = (string)p.Email
            };

            Func<Contact, Option<Person>> toOptionPerson = c => {
                if (c == null)
                    return None;
                else
                    return Some(new Person
                    {
                        FirstNames = c.Name.Split(' ').Head(),
                        LastName = string.Join(" ", c.Name.Split(' ').Tail()),
                        Email = (Email)c.Email
                    });
            };

            Person person = GetPerson();
            var elevatedPerson = Option<Person>.Some(person);//return - raise to elevated world
            
            Option<Person> defaultedPerson = elevatedPerson.Bind(EnsureValues);//bind - apply function and returns result
            Option<Contact> contact = defaultedPerson.Map(toContact);//map - 

            Option<Person> validatedPerson = ValidatePerson(person);

            Option<Person> p1 = contact.Bind(toOptionPerson);//bind - 
            p1.Match(
                Some: p => Console.WriteLine($"{p.FirstNames} {p.LastName} <{p.Email}>"),
                None: () => Console.WriteLine("Unknown")
            );

            Console.ReadKey();
        }

        private static Option<Person> EnsureValues(Person person)
        {
            if (string.IsNullOrEmpty(person.FirstNames))
                person.FirstNames = "John";

            if (string.IsNullOrEmpty(person.LastName))
                person.LastName = "Doe";

            if (string.IsNullOrEmpty(person.LastName))
                person.Email = (Email)"dead@dawn.com";

            return Some(person);
        }

        private static Contact FromPerson(Person person)
        {
            throw new NotImplementedException();
        }

        private static Option<Person> ValidatePerson(Person person)
        {
            var service = new PersonService();
            var validatedPerson = service.Validate(person);

            return validatedPerson.Match(
                Valid: p => {
                    Console.WriteLine("Valid");
                    return Some(p);
                },
                Invalid: err => {
                    err.ToList().ForEach(x => Console.WriteLine(x.Message));
                    return None;
                }
            );
        }

        private static Person GetPerson()
        {
            Email email = (Email)"test@test.com";

            var personRepository = new PersonRepository();
            var exceptionPerson = personRepository.Get(email);

            Person person = exceptionPerson.Match(
                Exception: ex => new Person(),
                Success: opt => opt.Match(
                    None: () => new Person(),
                    Some: p => p
                )
            );
            return person;
        }

        private static T Deserialize<T>(string json) where T : class
        {
            var obj = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                Converters = _converters
            });
            return obj;
        }

        private static void PrintAnEmail()
        {
            var email = (Email)"test@test.com";
            string e = Serialize(email);
            var s = (string)email;
            Console.WriteLine(e);
        }

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Converters = _converters
            });
        }

        static void DoSomeStuff(string lname, string fnames)
        {

        }

        static void DoSomeStuff(LastName lname, FirstNames fnames, Email email)
        {
            var person = new Person
            {
                FirstNames = fnames,
                LastName = lname,
                Email = email
            };

            var json = Serialize(person);
            Console.WriteLine(json);
            var p = Deserialize<Person>(json);
        }

        static List<JsonConverter> _converters = new List<JsonConverter>
                {
                    new SimpleJsonConverter<Email, string>(),
                    new SimpleJsonConverter<FirstNames, string>(),
                    new SimpleJsonConverter<LastName, string>()
                };
    }
}