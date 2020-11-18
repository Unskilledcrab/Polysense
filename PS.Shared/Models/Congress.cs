using PS.Shared.Models.Abstractions;

namespace PS.Shared.Models
{
    public class Congress : BaseEntity
    {
        /// <summary>
        /// Currently the 116th congress at the time of making this
        /// </summary>
        public int CurrentCongress { get; set; }

        public Senate Senate { get; set; }
        public HouseOfRepresentatives House { get; set; }
    }
}