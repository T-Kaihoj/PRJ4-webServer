using System.Collections.Generic;

namespace DAL.Models
{
    public interface IOutcome
    {
        long OutcomeId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        ICollection<IUser> Participants { get; set; }
    }
}