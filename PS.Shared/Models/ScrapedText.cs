using PS.Shared.Models.Abstractions;
using System.Collections.Generic;
using System.Text;

namespace PS.Shared.Models
{
    public class ScraperInput : BaseEntity
    {
        public string Website { get; set; }
        public string ScrapedText { get; set; }

        public string Classification
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var classification in Classifications)
                {
                    sb.Append($"{classification},");
                }
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
        }

        public ICollection<ScraperClassification> Classifications { get; set; }
    }

    public class ScraperClassification : BaseEntity
    {
        public string Classification { get; set; }
    }
}