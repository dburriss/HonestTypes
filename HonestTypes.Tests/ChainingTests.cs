using HonestTypes.Return;
using System;
using Xunit;

namespace HonestTypes.Tests
{
    using HonestTypes.Contacts;
    using static Fun;
    using static HonestTypes.Return.Validation;
    using LanguageExt;

    public class ChainingTests
    {
        Func<DateTime, Exceptional<string>> GetOrders = dt => { return new InvalidCastException(); };
        Func<string, Validation<string>> Validate = dt => { return Error("Not a valid addess"); };
        Func<string, Validation<string>> Parse = dt => { return Error("Too big"); };
        Func<string, Exceptional<string>> FtpUp = dt => { return new InvalidCastException(); };


        [Fact]
        public void Exceptional_OnException_SuccessIsFalse()
        {
            var example = new ThrowMyToys();
            LastName name = "BooBoo";
            //(Validate(name)).Map(Call(name));
            Validate("Something")
                .Bind(Parse);
        }


    }

    public static class Ext
    {
        public static Validation<LastName> Validate(this LastName @this)
        {
            if (string.IsNullOrEmpty(@this))
                return Invalid("no");
            else
                return @this;
        }

        public static Exceptional<LastName> Call(this LastName @this)
        {
            try
            {
                return @this;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }

}
