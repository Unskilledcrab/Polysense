using System;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public class Congress : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Politician> Members { get; set; }
        public int NumberOfPeople { get; set; }
    }
}