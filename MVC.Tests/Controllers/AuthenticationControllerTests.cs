using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Testing;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;
using Owin;

namespace MVC.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AuthenticationControllerTests : BaseRepositoryTest
    {
        private AuthenticationController uut;

        private IAuthenticationManager _authenticationManager;
        private UserManager<IdentityUser, string> _userManager;
        private IPasswordHasher _passwordHasher;
        private IStore _store;

        [SetUp]
        public void Setup()
        {
            _store = Substitute.For<IStore>();

            _userManager = new UserManager<IdentityUser, string>(_store);
            _authenticationManager = Substitute.For<IAuthenticationManager>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _userManager.PasswordHasher = _passwordHasher;

            // Create the unit under test.
            uut = new AuthenticationController(_userManager, _authenticationManager);

            // Create and set the controller context.
            var controllerContext = new ControllerContext();
            uut.ControllerContext = controllerContext;
        }

        [Test]
        public void SignIn_WithNonExistingUser_ReturnsError()
        {
            // Setup data.
            string userName = "test";
            string password = "password";

            var result = uut.SignIn(userName, password);

            Assert.That(uut.ModelState.IsValid, Is.True);

            Assert.That(result, Is.AssignableFrom<ViewResult>());
        }

        [Test]
        public void SignIn_WithExistingUserButWrongPassword_ReturnsError()
        {
            // Setup data.
            string userName = "test";
            string password = "password";
            string hash = "hash";

            var iUser = new IdentityUser()
            {
                UserName = userName
            };

            _store.FindByNameAsync(Arg.Is(userName)).Returns(iUser);
            _store.FindByIdAsync(Arg.Is(userName)).Returns(iUser);
            _store.GetLockoutEnabledAsync(Arg.Any<IdentityUser>()).Returns(Task.FromResult(false));
            _store.GetPasswordHashAsync(Arg.Is(iUser)).Returns(Task.FromResult(hash));
            _store.GetTwoFactorEnabledAsync(Arg.Any<IdentityUser>()).Returns(Task.FromResult(false));
            _passwordHasher.VerifyHashedPassword(Arg.Is(hash), Arg.Is(password)).Returns(PasswordVerificationResult.Success);

            // Set the password.
            //_userManager.p

            try
            {
                var result = uut.SignIn(userName, password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            

            //Assert.That(uut.ModelState.IsValid, Is.True);

            foreach (var v in _store.ReceivedCalls())
            {
                Console.WriteLine(v.GetMethodInfo().Name);
            }
            foreach (var v in _passwordHasher.ReceivedCalls())
            {
                Console.WriteLine(v.GetMethodInfo().Name);
            }
            foreach (var v in _authenticationManager.ReceivedCalls())
            {
                Console.WriteLine(v.GetMethodInfo().Name);
            }
            /*
             * 
FindByNameAsync
FindByIdAsync
GetLockoutEnabledAsync
GetPasswordHashAsync
FindByIdAsync
FindByIdAsync
GetAccessFailedCountAsync
FindByIdAsync
GetTwoFactorEnabledAsync
VerifyHashedPassword
SignOut
SignIn
             */

            //Assert.That(result, Is.AssignableFrom<ViewResult>());
        }

        [Test]
        public void SignIn_WithExistingUser_SignsInAndRedirects()
        {
            // Setup data.
            string userName = "test";
            string password = "password";



            // Setup the authentication manager.
            

            var result = uut.SignIn(userName, password);

            Assert.That(uut.ModelState.IsValid, Is.True);

            foreach (var v in _authenticationManager.ReceivedCalls())
            {
                Console.WriteLine(v.GetMethodInfo().Name);
            }

            Assert.That(result, Is.AssignableFrom<RedirectResult>());
        }
    }
}

