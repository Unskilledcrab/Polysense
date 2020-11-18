using PS.Shared.Models.Abstractions;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public class CongressionalCommitte : BaseEntity
    {
        public string Name { get; set; }
        public Politician Chair { get; set; }
        public ICollection<Politician> Members { get; set; }
        public ICollection<Bill> ProposedBills { get; set; }
    }
}