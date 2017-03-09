using HonestTypes.Contacts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DemoHonestTypes
{

    public class StringSerializer : JsonConverter
    {
        private readonly IDictionary<Type, Tuple<Func<object, string>, Func<string, object>>> conversions;
        public StringSerializer()
        {
            conversions = new Dictionary<Type, Tuple<Func<object, string>, Func<string, object>>>
            {
                { typeof(Email), Tuple(obj => obj.ToString(), s => (Email)s) },
                { typeof(FirstNames), Tuple(obj => obj.ToString(), s => (FirstNames)s) },
                { typeof(LastName), Tuple(obj => obj.ToString(), s => (LastName)s) }
            };
        }

        Tuple<Func<object, string>, Func<string, object>> Tuple(Func<object, string> toString, Func<string, object> toObject)
        {
            return new Tuple<Func<object, string>, Func<string, object>>(toString, toObject);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var converters = conversions[value.GetType()];
            var s = converters.Item1(value);
            writer.WriteValue(s);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.Value != null)
            {
                var converters = conversions[objectType];
                var obj = converters.Item2(reader.Value.ToString());
                return obj;
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            var can = conversions.ContainsKey(objectType);
            return can;
        }
    }
}
