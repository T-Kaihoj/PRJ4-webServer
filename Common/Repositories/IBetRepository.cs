using Common.Models;

namespace Common.Repositories
{
    public interface IBetRepository : IRepository<Bet>
    {
        Bet GetEager(long id);
    }
}
