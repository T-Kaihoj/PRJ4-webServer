using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public interface IBet
    {
        long BetId { get; set; }
        string Name { get; set; }
        DateTime StartDate { get; set; }
        DateTime StopDate { get; set; }
        IOutcome Result { get; set; }
        string Description { get; set; }
        Decimal BuyIn { get; set; }
        Decimal Pot { get; set; }
        ICollection<IUser> Participants { get; set; }
        ICollection<IOutcome> Outcomes { get; set; }
        IUser Judge { get; set; }
        List<IUser> Invited { get; set; }
    }
}