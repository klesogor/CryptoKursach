using Bot.Bot.Replies.Interfaces;
using Bot.Exceptions;
using Bot.Routers;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotTests.Tests
{
    sealed class RouterTest
    {

        private IRouter _router;
        private Chat _chat;

        [SetUp]
        public void Boot()
        {
            _router = new Router(new RouteExpressionParser());
            _chat = new Mock<Chat>().Object;
        }

        [Test]
        public async Task TestShouldHandleBasicDispatch()
        {
            bool called = false;
            _router.Bind("/test", (pbag, chat) => {
                called = true;
                return Task.FromResult(new Mock<IReply>().Object);
            });

            await _router.Dispatch("/test", _chat);

            Assert.IsTrue(called);
        }

        public async Task TestShouldDispatchRightRoute()
        {
            bool called = false;

            _router.Bind("/test-fail-1", (pbag, chat) => throw new Exception("Invalid route called"));
            _router.Bind("/test-success", (pbag, chat) => {
                called = true;
                return Task.FromResult(new Mock<IReply>().Object);
            });
            _router.Bind("/test-fail-2", (pbag, chat) => throw new Exception("Invalid route called"));


            await _router.Dispatch("/test-success", _chat);

            Assert.IsTrue(called);
        }

        public void TestShouldThrowExceptionForUnbindedRoute()
        {
            _router.Bind("/test", (pbag, chat) => {
                return Task.FromResult(new Mock<IReply>().Object);
            });

            Assert.ThrowsAsync<RouteException>(
                    async () => await _router.Dispatch("/test-unexisting-route", _chat)
            );
        }

        public async Task TestShouldInjectArguments()
        {
            bool called = false;

            _router.Bind("/test {param} testParam={param2}", (pbag, chat) => {
                Assert.Equals("notNull",pbag.GetObjectAsString("param"));
                Assert.Equals("SomethingElse", pbag.GetObjectAsString("param2"));
                called = true;
                return Task.FromResult(new Mock<IReply>().Object);
            });

            await _router.Dispatch("/test notNull param2=SomethingElse", _chat);

            Assert.IsTrue(called);
        }

        public async Task TestShouldUseCorrectHandlerForRouteWithAndWithoutParams()
        {
            bool called = false;
            _router.Bind("/test", (pbag, chat) => throw new Exception("Invalid route called"));
            _router.Bind("/test {test}", (pbag, chat) => {
                called = true;
                return Task.FromResult(new Mock<IReply>().Object);
            });

            await _router.Dispatch("/test test", _chat);

            Assert.IsTrue(called);
        }
    }
}
