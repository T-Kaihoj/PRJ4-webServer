using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Common;
using Common.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public abstract class BaseRepositoryTest
    {
        protected IFactory Factory;
        protected IUnitOfWork MyWork;
        protected IBetRepository BetRepository;
        protected ILobbyRepository LobbyRepository;
        protected IOutcomeRepository OutcomeRepository;
        protected IUserRepository UserRepository;

        [SetUp]
        public void BaseSetup()
        {
            // Create substitutes for the repositories.
            BetRepository = Substitute.For<IBetRepository>();
            LobbyRepository = Substitute.For<ILobbyRepository>();
            OutcomeRepository = Substitute.For<IOutcomeRepository>();
            UserRepository = Substitute.For<IUserRepository>();

            // Create a unit of work substitute.
            MyWork = Substitute.For<IUnitOfWork>();

            // Register the repositories with the unit of work.
            MyWork.Bet.Returns(BetRepository);
            MyWork.Lobby.Returns(LobbyRepository);
            MyWork.Outcome.Returns(OutcomeRepository);
            MyWork.User.Returns(UserRepository);

            // Create a substitue for the factory.
            Factory = Substitute.For<IFactory>();

            // Register the unit of work with the factory.
            Factory.GetUOF().Returns(MyWork);
        }

        protected void CheckStatusCode(object result, int code)
        {
            Assert.That(result, Is.InstanceOf<HttpStatusCodeResult>());

            var codeResult = result as HttpStatusCodeResult;

            Assert.That(codeResult.StatusCode, Is.EqualTo(code));
        }

        protected T CheckViewModel<T>(object result)
        {
            Assert.That(result, Is.TypeOf<ViewResult>());

            var view = result as ViewResult;

            Assert.That(view.Model, Is.TypeOf<T>());

            return (T)view.Model;
        }

        protected void CheckViewName(object result, string viewName)
        {
            Assert.That(result, Is.TypeOf<ViewResult>());

            var view = result as ViewResult;

            Assert.That(view.ViewName, Is.EqualTo(viewName));
        }
    }
}
