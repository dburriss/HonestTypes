using HonestTypes.Return;
using System;
using System.Linq;
using Xunit;

namespace HonestTypes.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void Validation_OnError_IsValidIsFalse()
        {
            var example = new ThrowMyToys();
            var v = example.ValidateName(null);
            Assert.False(v.IsValid);
        }

        [Fact]
        public void Validation_OnInvalid_IsValidIsFalse()
        {
            var example = new ThrowMyToys();
            var v = example.ValidateName("foo");
            Assert.False(v.IsValid);
        }

        [Fact]
        public void Validation_OnNoError_IsValidIsTrue()
        {
            var example = new ThrowMyToys();
            var v = example.ValidateName("BooBoo");
            Assert.True(v.IsValid);
        }

        [Fact]
        public void Validation_OnError_DoesNotReturnValue()
        {
            var example = new ThrowMyToys();
            var v = example.ValidateName("FooBoo");//returns the value if valid
            Assert.NotEqual("FooBoo", v);
        }

        [Fact]
        public void Validation_OnError_ContainsErrors()
        {
            var example = new ThrowMyToys();
            Validation<string> v = example.ValidateName("fooBoo");
            v.Match(
                Invalid: errors => Assert.Equal(2, errors.Count()),
                Valid: str => Assert.True(false)
            );
        }

        [Fact]
        public void Validation_OnNoError_ReturnsValue()
        {
            var example = new ThrowMyToys();
            var v = example.ValidateName("BooBoo");//returns the value if valid
            Assert.Equal("BooBoo", v);
        }

        [Fact]
        public void GetOrElse_OnError_ReturnsDefault()
        {
            var example = new ThrowMyToys();
            var v = example.ValidateName("FooBoo");
            var result = v.GetOrElse("Bob");
            Assert.Equal("Bob", result);
        }

        [Fact]
        public void GetOrElse_OnNoError_ReturnsValue()
        {
            var example = new ThrowMyToys();
            var v = example.ValidateName("BooBoo");
            var result = v.GetOrElse("Bob");
            Assert.Equal("BooBoo", result);
        }

        [Fact]
        public void GetOrElse_OnError_CallsFallback()
        {
            var result = false;
            Func<string> fallback = () => {
                result = true;
                return "Bob";
            };
            var example = new ThrowMyToys();
            var v = example.ValidateName("FooBoo");

            var x = v.GetOrElse(fallback);

            Assert.True(result);
        }

        [Fact]
        public void Join_InvalidWithValid_IsInvalid()
        {
            var sut = Fun.Invalid<string>("This is invalid.");

            var result = sut.Join(Fun.Valid("Hello"));

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Join_ValidWithInvalid_IsInvalid()
        {
            var sut = Fun.Valid("Hello");

            var result = sut.Join(Fun.Invalid("This is invalid."));

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Join_InvalidWithInvalid_IsInvalid()
        {
            var sut = Fun.Invalid<string>("This is invalid");

            var result = sut.Join(Fun.Invalid("This is also invalid."));

            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void Join_InvalidWithInvalid_ContainsAllErrors()
        {
            var sut = Fun.Invalid<string>("This is invalid.");

            var result = sut.Join(Fun.Invalid("This is also invalid."));

            var errors = result.Match(
                Invalid: e => e,
                Valid: _ => Enumerable.Empty<Error>()
            );
            
            Assert.Contains(Fun.Error("This is invalid."), errors);
            Assert.Contains(Fun.Error("This is also invalid."), errors);
        }

        [Fact]
        public void Join_ValidWithValid_IsValid()
        {
            var sut = Fun.Valid("Hello");

            var result = sut.Join(Fun.Valid("Hello"));

            Assert.True(result.IsValid);
        }
    }
}
