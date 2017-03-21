using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class Outcome : IOutcome
    {
        [Key]
        public long OutcomeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<IUser> Participants { get; set; }
    }
}