using HonestTypes.Return;
using Xunit;

namespace HonestTypes.Tests
{
    public  class ErrorTests
    {
        [Fact]
        public void Error_OnError_LeftIsTrue()
        {
            var example = new ThrowMyToys();
            var x = example.CallName("foo");
            Assert.True(x.IsLeft);
            Assert.False(x.IsRight);
        }

        [Fact]
        public void Error_OnNoError_RightIsTrue()
        {
            var example = new ThrowMyToys();
            var x = example.CallName("BooBoo");
            Assert.True(x.IsRight);
            Assert.False(x.IsLeft);
        }

        [Fact]
        public void Error_OnError_MatchesLeft()
        {
            var example = new ThrowMyToys();
            var x = example.CallName("foo");
            x.Match(
                Right: s => Assert.True(false),
                Left: err => Assert.True(true)//expecting this one
            );
        }

        [Fact]
        public void Error_OnError_ReturnsError()
        {
            var example = new ThrowMyToys();
            var x = example.CallName("foo");
            x.Match(
                Right: s => Assert.True(false),
                Left: err => Assert.IsType<Error>(err)
            );
        }

        [Fact]
        public void Error_OnNoError_MatchesRight()
        {
            var example = new ThrowMyToys();
            var x = example.CallName("BooBoo");
            x.Match(
                Right: s => Assert.True(true),
                Left: err => Assert.True(false)
            );
        }

        [Fact]
        public void Error_OnNoError_ReturnsHaha()
        {
            var example = new ThrowMyToys();
            var x = example.CallName("BooBoo");
            x.Match(
                Right: s => Assert.Equal("Haha", s),
                Left: err => Assert.True(false)
            );
        }
    }
}
