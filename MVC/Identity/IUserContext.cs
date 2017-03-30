using System.Security.Principal;

namespace MVC.Identity
{
    /// <summary>
    /// Wrapper interface, to allow for dependency injection in the constructor, but still avoiding injecting runtime information.
    /// This is merely meant to call the HttpContext functionts, which are runtime dependent, but allows us to inject it for tests.
    /// </summary>
    public interface IUserContext
    {
        IIdentity Identity { get; }
    }
}