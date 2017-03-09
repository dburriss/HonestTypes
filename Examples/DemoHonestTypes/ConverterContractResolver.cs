using HonestTypes.Contacts;
using Newtonsoft.Json.Serialization;
using System;

namespace DemoHonestTypes
{
    public class HonestTypeContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if (objectType == typeof(Email) )
            {
                contract.Converter = new StringSerializer();
            }

            return contract;
        }
    }
}
