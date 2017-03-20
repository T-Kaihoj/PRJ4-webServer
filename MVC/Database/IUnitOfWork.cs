using System;
using MVC.Database.Repositories;

namespace MVC.Database

{
    public interface IUnitOfWork : IDisposable
    {
        IBetRepository Bet { get; }
        ILobbyRepository Lobby { get; }
        IUserRepository User { get; }

        int Complete();
    }
}
