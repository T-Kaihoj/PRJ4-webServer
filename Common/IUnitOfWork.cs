using System;
using DAL.Repositories;

namespace DAL

{
    public interface IUnitOfWork : IDisposable
    {
        IBetRepository Bet { get; }
        ILobbyRepository Lobby { get; }
        IUserRepository User { get; }

        int Complete();
    }
}
