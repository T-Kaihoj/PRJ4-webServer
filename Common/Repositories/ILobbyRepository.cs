﻿using Common.Models;

namespace Common.Repositories
{
    public interface ILobbyRepository : IRepository<Lobby>
    {
        Lobby GetEager(long id);
    }
}
