using System;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public class Bill : BaseEntity
    {
        public ICollection<Politician> Cosponsors { get; set; }
        public DateTime IntroductionDatetime { get; set; }
        public string Name { get; set; }
        public Politician Sponsor { get; set; }
    }
}