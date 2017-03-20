using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Core.Repositories;

namespace Models.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBetRepository Bet { get; }
        ILobbyRepository Lobby { get; }
        IUserRepository User { get; }

        int Complete();
    }
}
