using PS.Shared.Models;

namespace PS.Shared.Extensions
{
    public static class ScraperTextExtensions
    {
        public static string ToColorKey(this ScraperText source)
        {
            var category = source.Category.Name;
            if (category.Equals("Uncategorized"))
                return "High";
            else
                return "Low";
        }

        public static string GetDomainName(this ScraperText source)
        {
            var startIndex = source.Website.IndexOf("://") + 3;
            var endIndex = source.Website.IndexOf(".com");
            return source.Website.Substring(startIndex, (endIndex - startIndex));
        }
    }
}