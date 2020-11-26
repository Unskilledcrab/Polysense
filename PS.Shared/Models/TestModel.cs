using PS.Shared.Models.Abstractions;

namespace PS.Shared.Models
{
    public class TestModel : BaseEntity
    {
        public int SomeNumber { get; set; }
        public string SomeText { get; set; }
    }
}