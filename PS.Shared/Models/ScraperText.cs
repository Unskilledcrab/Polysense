using PS.Shared.Models.Abstractions;

namespace PS.Shared.Models
{
    public class ScraperText : BaseEntity
    {
        public string Website { get; set; }
        public string Text { get; set; }
    }
}