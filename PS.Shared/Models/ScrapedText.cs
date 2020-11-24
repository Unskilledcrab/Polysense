using PS.Shared.Models.Abstractions;
using System.Collections.Generic;

namespace PS.Shared.Models
{
    public class ScraperInput : BaseEntity
    {
        public string Website { get; set; }
        public string ScrapedText { get; set; }

        public int ClassificationEncoding
        {
            get
            {
                int encoding = 0;
                foreach (var item in Classification)
                {
                    encoding += item.ClassificationEncoding;
                }
                return encoding;
            }
        }

        public ICollection<ScraperClassification> Classification { get; set; }
    }

    public class ScraperClassification : BaseEntity
    {
        public string Classification { get; set; }

        /// <summary>
        /// This is last highest classification encoding plus the last highest classification
        /// encoding (binary encoding)
        /// </summary>
        public int ClassificationEncoding { get; set; }
    }
}