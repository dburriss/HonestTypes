using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HonestTypes.Serialization
{
    public class SimpleJsonConverter<Complex, Simple> : JsonConverter where Complex : class
    {
        private readonly IDictionary<Type, Action<JsonSerializer, JsonWriter, Complex>> conversions;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var complex = value as Complex;
            if (complex == null)
                return;

            Action<JsonSerializer, JsonWriter, Complex> serialize = null;
            if (!conversions.TryGetValue(typeof(Simple), out serialize))
            {
                throw new InvalidOperationException($"Could not write json for {value.ToString()}");
            }

            serialize(serializer, writer, complex);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var v = Activator.CreateInstance(objectType, reader.Value);//(Complex)reader.Value;
            return v;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Complex) == objectType;
        }

        public SimpleJsonConverter()
        {
            conversions = new Dictionary<Type, Action<JsonSerializer, JsonWriter, Complex>>
            {
                //{ typeof(sbyte?), (s, w, c) => s.Serialize(w, Convert.ToSByte(c)) },
                //{ typeof(short?), (s, w, c) => w.WriteValue(Convert.ToInt16(c)) },
                //{ typeof(ushort?), (s, w, c) => w.WriteValue(Convert.ToUInt16(c)) },
                //{ typeof(string), (s, w, c) => s.Serialize(w, Convert.ToString(c)) }
                { typeof(string), (s, w, c) => s.Serialize(w, Convert.ToString(c)) }
            };
        }

        //public virtual void WriteValue(JsonWriter writer, char? value);

        //public virtual void WriteValue(JsonWriter writer, byte? value);

        //public virtual void WriteValue(JsonWriter writer, byte[] value);

        //public virtual void WriteValue(JsonWriter writer, DateTime? value);

        //public virtual void WriteValue(JsonWriter writer, DateTimeOffset? value);

        //public virtual void WriteValue(JsonWriter writer, Guid? value);

        //public virtual void WriteValue(JsonWriter writer, TimeSpan? value);

        //public virtual void WriteValue(JsonWriter writer, Uri value);

        //public virtual void WriteValue(JsonWriter writer, object value);

        //public virtual void WriteValue(JsonWriter writer, bool? value);

        //public virtual void WriteValue(JsonWriter writer, decimal? value);

        //public virtual void WriteValue(JsonWriter writer, double? value);

        //[CLSCompliant(JsonWriter writer, false)]
        //public virtual void WriteValue(JsonWriter writer, uint? value);

        //[CLSCompliant(JsonWriter writer, false)]
        //public virtual void WriteValue(JsonWriter writer, ulong? value);

        //public virtual void WriteValue(JsonWriter writer, string value)
        //{

        //}

        //public virtual void WriteValue(JsonWriter writer, int value);

        //[CLSCompliant(JsonWriter writer, false)]
        //public virtual void WriteValue(JsonWriter writer, uint value);

        //public virtual void WriteValue(JsonWriter writer, long value);

        //[CLSCompliant(JsonWriter writer, false)]
        //public virtual void WriteValue(JsonWriter writer, ulong value);

        //public virtual void WriteValue(JsonWriter writer, float? value);

        //public virtual void WriteValue(JsonWriter writer, double value);

        //public virtual void WriteValue(JsonWriter writer, bool value);

        //public virtual void WriteValue(JsonWriter writer, short value);

        //public virtual void WriteValue(JsonWriter writer, float value);

        //public virtual void WriteValue(JsonWriter writer, char value);

        //public virtual void WriteValue(JsonWriter writer, long? value);

        //public virtual void WriteValue(JsonWriter writer, int? value);

        //public virtual void WriteValue(JsonWriter writer, TimeSpan value);

        //[CLSCompliant(JsonWriter writer, false)]
        //public virtual void WriteValue(JsonWriter writer, ushort value);

        //public virtual void WriteValue(JsonWriter writer, DateTimeOffset value);

        //public virtual void WriteValue(JsonWriter writer, Guid value);

        //public virtual void WriteValue(JsonWriter writer, decimal value);

        //[CLSCompliant(JsonWriter writer, false)]
        //public virtual void WriteValue(JsonWriter writer, sbyte value);

        //public virtual void WriteValue(JsonWriter writer, byte value);

        //public virtual void WriteValue(JsonWriter writer, DateTime value);
    }
}
