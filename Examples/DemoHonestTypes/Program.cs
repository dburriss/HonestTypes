using HonestTypes.Contacts;
using HonestTypes.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DemoHonestTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintAnEmail();

            FirstNames name = "Devon Aragorn";
            string nameAsString = name;
            Console.WriteLine(nameAsString);
            Email email = (Email)"test@test.com";
            string emailAsString = (string)email;
            Console.WriteLine(emailAsString);
            string n = Serialize(name);

            FirstNames fnamesObj = Deserialize<FirstNames>(n);
            Console.WriteLine(n);

            DoSomeStuff("Burriss", name, email);

            Console.ReadKey();
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