using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Shared.Models
{
    public class Bill : BaseEntity
    {
        public IEnumerable<Politician> Cosponsors { get; set; }
        public DateTime IntroductionDatetime { get; set; }
        public string Name { get; set; }
        public Politician Sponsor { get; set; }
    }
}