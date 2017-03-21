using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Bet : IBet
    {
        [Key]
        public long BetId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public IOutcome Result { get; set; }
        public string Description { get; set; }
        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public virtual ICollection<IUser> Participants { get; set; }
        public virtual ICollection<IOutcome> Outcomes { get; set; }
        public virtual IUser Judge { get; set; }
        
        
        public List<IUser> Invited { get; set; }

        internal static object getBet(int id)
        {
            throw new NotImplementedException();
        }

        
    }

}
