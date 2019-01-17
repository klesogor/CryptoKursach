using Bot.APIs;
using Moq;
using NUnit.Framework;

namespace BotTests.Tests
{
    class CacherTest
    {
        private Mock<IAPI> _mock;
        private ApiCacher _cacher;

        [SetUp]
        public void SetUp()
        {
            var moq = new Mock<IAPI>();
            moq.Setup(x => x.GetAvailableCurrencies());
            moq.Setup(x => x.GetSubscriptions(It.IsAny<int>()));
            moq.Setup(x => x.GetCurrencyRate(It.IsAny<int>(), It.IsAny<int>()));

            _cacher = new ApiCacher(moq.Object);
            _mock = moq;
        }

        [Test]
        public void TestCachableCalledOnlyOnce()
        {
            var res = _cacher.GetAvailableCurrencies();
            var res2 = _cacher.GetAvailableCurrencies();

            _mock.Verify(x => x.GetAvailableCurrencies(), Times.Once);
        }

        [Test]
        public void TestNoncachableCalledEveryTime()
        {
            var res = _cacher.GetSubscriptions(1);
            var res2 = _cacher.GetSubscriptions(1);

            _mock.Verify(x => x.GetSubscriptions(1), Times.Exactly(2));
        }

        [Test]
        public void TestCacheArguments()
        {
            var res = _cacher.GetCurrencyRate(1, 1);
            var res2 = _cacher.GetCurrencyRate(1, 1);

            _mock.Verify(x => x.GetCurrencyRate(1,1), Times.Once);
        }
    }
}
