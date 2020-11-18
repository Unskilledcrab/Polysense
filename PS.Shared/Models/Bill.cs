using PS.Shared.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PS.Shared.Models
{
    public class Bill : BaseEntity
    {
        public ICollection<Politician> Cosponsors { get; set; }
        public DateTime IntroductionDatetime { get; set; }
        public string Name { get; set; }
        public Politician Sponsor { get; set; }

        /// <summary>
        /// When this is set it needs to update this property and add the status to the Bill Status table
        /// </summary>
        public BillStatus CurrentStatus => HistoricalStatuses.OrderBy(s => s.TransitionDate).FirstOrDefault();

        public ICollection<BillStatus> HistoricalStatuses { get; set; }
    }

    public class BillVotes : BaseEntity
    {
        public Congress Congress { get; set; }
    }
}