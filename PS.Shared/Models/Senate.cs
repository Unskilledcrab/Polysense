using PS.Shared.Models.Abstractions;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public class Senate : BaseEntity
    {
        //https://www.govtrack.us/congress/members/current#current_role_type=1
        public ICollection<Politician> Senators { get; set; }

        public ICollection<Bill> Bills { get; set; }
        public Politician PresidentOfTheSenate { get; set; }
    }
}