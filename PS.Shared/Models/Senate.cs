using PS.Shared.Models.Abstractions;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public class Senate : BaseEntity
    {
        public ICollection<Politician> Senators { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public Politician PresidentOfTheSenate { get; set; }
    }
}