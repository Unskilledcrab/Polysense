using PS.Shared.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public enum SupremeCourtTransitionType
    {
        LossingJustice,
        GainingJustice
    }

    public class SupremeCourt : BaseEntity
    {
        public ICollection<Justice> Justices { get; set; }
        public int Size => Justices.Count;
        public Justice ChiefJustice { get; set; }

        /// <summary>
        /// This is the date that the new Justice was appointed to the supreme court or that a
        /// Justice left the court on
        /// </summary>
        public DateTime TransitionDate { get; set; }

        public SupremeCourtTransitionType TransitionType { get; set; }
    }
}