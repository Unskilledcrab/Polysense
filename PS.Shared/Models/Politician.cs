using PS.Shared.Models.Abstractions;

namespace PS.Shared.Models
{
    public enum PoliticalOfficeType
    {
        NA,
        Mayor,
        TownCouncil,
        CityCouncil,
        CountyCommissioner,
        Govenor,
        LieutenantGovernor,
        SecretaryOfState,
        AttorneyGeneral,
        Comptroller,
        Treasurer,
        StateSenator,
        StateLegislator,
        President,
        VicePresident
    }

    public class Politician : Person
    {
        public string Name { get; set; }

        public PoliticalOfficeType CurrentOffice { get; set; }
    }
}