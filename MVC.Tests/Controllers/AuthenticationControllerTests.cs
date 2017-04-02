using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MVC.Controllers;
using MVC.Identity;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AuthenticationControllerTests : BaseRepositoryTest
    {
        private AuthenticationController uut;

        private UserManager<IdentityUser, string> _userManager;
        private IPasswordHasher _passwordHasher;
        private IStore _store;
        private IAuthenticationManager _authenticationManager;

        private string hash;
        private string password;
        private string userName;

        [SetUp]
        public void Setup()
        {
            _store = Substitute.For<IStore>();

            _userManager = new UserManager<IdentityUser, string>(_store);
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _userManager.PasswordHasher = _passwordHasher;
            _authenticationManager = Substitute.For<IAuthenticationManager>();

            // Create the unit under test.
            uut = new AuthenticationController(_userManager, _authenticationManager);

            // Create and set the controller context.
            var controllerContext = new ControllerContext();
            uut.ControllerContext = controllerContext;

            // Setup data.
            userName = "test";
            password = "password";
            hash = "hash";

            var iUser = new IdentityUser()
            {
                UserName = userName
            };

            _store.FindByNameAsync(Arg.Is(userName)).Returns(iUser);
            _store.FindByIdAsync(Arg.Is(userName)).Returns(iUser);
            _store.GetLockoutEnabledAsync(Arg.Any<IdentityUser>()).Returns(Task.FromResult(false));
            _store.GetPasswordHashAsync(Arg.Is(iUser)).Returns(Task.FromResult(hash));
            _store.GetTwoFactorEnabledAsync(Arg.Any<IdentityUser>()).Returns(Task.FromResult(false));
        }

        #region SignIn

        [Test]
        public void SignIn_CallsUserStore()
        {
            _passwordHasher.VerifyHashedPassword(Arg.Is(hash), Arg.Is(password)).Returns(PasswordVerificationResult.Success);

            _store.DidNotReceive().FindByNameAsync(Arg.Any<string>());
            _store.DidNotReceive().FindByIdAsync(Arg.Any<string>());
            _store.DidNotReceive().GetPasswordHashAsync(Arg.Any<IdentityUser>());
            _passwordHasher.DidNotReceive().VerifyHashedPassword(Arg.Any<string>(), Arg.Any<string>());
            _authenticationManager.DidNotReceive().SignOut(Arg.Any<string[]>());
            _authenticationManager.DidNotReceive().SignIn(Arg.Any<AuthenticationProperties>(), Arg.Any<ClaimsIdentity[]>());

            // Attempt the signin.
            var result = uut.SignIn(userName, password);

            // Check we have received the calls we expected.
            _store.Received(1).FindByNameAsync(Arg.Any<string>());
            _store.Received().FindByIdAsync(Arg.Any<string>());
            _store.Received(1).GetPasswordHashAsync(Arg.Any<IdentityUser>());
            _passwordHasher.Received(1).VerifyHashedPassword(Arg.Any<string>(), Arg.Any<string>());
            _authenticationManager.Received(1).SignOut(Arg.Any<string[]>());
            _authenticationManager.Received(1).SignIn(Arg.Any<AuthenticationProperties>(), Arg.Any<ClaimsIdentity[]>());
        }

        [TestCase("")]
        [TestCase(" ")]
        public void SignIn_WithInvalidPassword_ReturnsError(string input)
        {
            // Attempt the signin.
            var result = uut.SignIn(userName, input);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("InvalidCredentials"));
        }

        [TestCase("")]
        [TestCase(" ")]
        public void SignIn_WithInvalidUserName_ReturnsError(string input)
        {
            // Attempt the signin.
            var result = uut.SignIn(input, password);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("InvalidCredentials"));
        }

        [Test]
        public void SignIn_WithWrongData_ReturnsError()
        {
            _passwordHasher.VerifyHashedPassword(Arg.Is(hash), Arg.Is(password)).Returns(PasswordVerificationResult.Failed);

            // Attempt the signin.
            var result = uut.SignIn(userName, password);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("InvalidCredentials"));
        }

        [Test]
        public void SignIn_WithValidData_Redirects()
        {
            
            _passwordHasher.VerifyHashedPassword(Arg.Is(hash), Arg.Is(password)).Returns(PasswordVerificationResult.Success);

            // Attempt the signin.
            var result = uut.SignIn(userName, password);

            Assert.That(result, Is.TypeOf<RedirectResult>());

            var rResult = result as RedirectResult;

            Assert.That(rResult.Permanent, Is.False);
            Assert.That(rResult.Url, Is.EqualTo("/"));
        }

        #endregion

        #region SignOut

        [Test]
        public void SignOut_CallsAuthenticationManager()
        {
            _authenticationManager.DidNotReceive().SignOut(Arg.Any<string[]>());

            // Attempt the signout.
            var result = uut.SignOut();

            _authenticationManager.Received(1).SignOut(Arg.Any<string[]>());

            Assert.That(result.Permanent, Is.False);
            Assert.That(result.Url, Is.EqualTo("/"));
        }

        #endregion

        #region Old

        /*
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
            

            Assert.That(result, Is.AssignableFrom<RedirectResult>());
        }*/

        #endregion
    }
}

