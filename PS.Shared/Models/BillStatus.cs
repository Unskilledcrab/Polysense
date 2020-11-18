using PS.Shared.Models.Abstractions;
using System;

namespace PS.Shared.Models
{
    public enum BillStatusType
    {
        Introduced,
        OrderedReported,
        OnHouseSchedule,
        TextPublished,
        PassedHouse,
        PassedSenate,
        SignedByPresident
    }

    public class BillStatus : BaseEntity
    {
        public DateTime TransitionDate { get; set; }
        public BillStatusType Status { get; set; }
        public Bill Bill { get; set; }
    }
}