using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Database.Models
{
    public class Outcome
    {
        [Key]
        public long OutcomeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Participants { get; set; }
    }
}