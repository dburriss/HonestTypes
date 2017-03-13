using HonestTypes.Return;
using System;
using Xunit;

namespace HonestTypes.Tests
{
    public class ExceptionalTests
    {
        [Fact]
        public void Exceptional_OnException_SuccessIsFalse()
        {
            var example = new ThrowMyToys();
            var x = example.ThrowAnException();
            Assert.False(x.Success);
            Assert.True(x.Exception);
        }

        [Fact]
        public void Exceptional_OnException_Exception()
        {
            var example = new ThrowMyToys();
            var x = example.ThrowAnException();
            x.Match(
                Exception: ex => Assert.IsType<NullReferenceException>(ex),
                Success: s => Assert.True(false)//force fail if this is called
            );
        }

        [Fact]
        public void Exceptional_OnNoException_SuccessIsFalse()
        {
            var example = new ThrowMyToys();
            var x = example.ReturnAResult();
            Assert.True(x.Success);
            Assert.False(x.Exception);
        }

        [Fact]
        public void Exceptional_OnNoException_Exception()
        {
            var example = new ThrowMyToys();
            Exceptional<string> x = example.ReturnAResult();
            x.Match(
                Exception: ex => Assert.IsType<NullReferenceException>(ex),
                Success: s => Assert.Equal("Xyz", s)
            );
        }
    }
}
