using HonestTypes.Contacts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace HonestTypes.Serialization.Tests
{
    public class SimpleJsonConverterTests
    {
        [Fact]
        public void Serialize_String_SerializesToJson()
        {
            var json = Serialize("test");
            Assert.Equal("\"test\"", json);
        }

        [Fact]
        public void Serialize_ImplicitType_SerializesToJson()
        {
            LastName lname = "Burriss";
            var json = Serialize(lname);
            Assert.Equal("\"Burriss\"", json);
        }

        [Fact]
        public void Serialize_ExplicitType_SerializesToJson()
        {
            Email email = (Email)"test@test.com";
            var json = Serialize(email);
            Assert.Equal("\"test@test.com\"", json);
        }

        [Fact]
        public void Serialize_CompositeType_SerializesToJson()
        {
            Test test = new Test
            {
                LastName = "Burriss",
                Email = (Email)"test@test.com"
            };
            var json = Serialize(test);
            Assert.Equal("{\"LastName\":\"Burriss\",\"Email\":\"test@test.com\"}", json);
        }

        [Fact]
        public void Deserialize_JsonString_SerializesToJson()
        {
            var result = Deserialize<string>("\"test\"");
            Assert.Equal("test", result);
        }

        [Fact]
        public void Deserialize_JsonImplicitType_SerializesToJson()
        {
            LastName lname = "Burriss";
            var result = Deserialize<LastName>("\"Burriss\"");
            Assert.Equal(lname, result);
        }

        [Fact]
        public void Deserialize_JsonExplicitType_SerializesToJson()
        {
            Email email = (Email)"test@test.com";
            var result = Deserialize<Email>("\"test@test.com\"");
            Assert.Equal(email, result);
        }

        [Fact]
        public void Deserialize_JsonCompositeType_SerializesToJson()
        {
            Test test = new Test
            {
                LastName = "Burriss",
                Email = (Email)"test@test.com"
            };
            var result = Deserialize<Test>("{\"LastName\":\"Burriss\",\"Email\":\"test@test.com\"}");
            Assert.Equal(test.LastName, result.LastName);
            Assert.Equal(test.Email, result.Email);
        }

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Converters = _converters
            });
        }

        private static T Deserialize<T>(string json) where T : class
        {
            var obj = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                Converters = _converters
            });
            return obj;
        }

        static List<JsonConverter> _converters = new List<JsonConverter>
                {
                    new SimpleJsonConverter<Email, string>(),
                    new SimpleJsonConverter<FirstNames, string>(),
                    new SimpleJsonConverter<LastName, string>()
                };
    }
}
