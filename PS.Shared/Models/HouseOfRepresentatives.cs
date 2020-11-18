using PS.Shared.Models.Abstractions;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public class HouseOfRepresentatives : BaseEntity
    {
        public ICollection<Politician> CongressMen { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public Politician SpeakerOfTheHouse { get; set; }
    }
}