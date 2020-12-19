using System.Text;

namespace PS.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string GetWords(this string source, int numberOfWords)
        {
            var sb = new StringBuilder();
            var words = source.Split(' ');
            for (int i = 0; i < numberOfWords; i++)
            {
                sb = sb.Append($"{words[i]} ");
            }
            sb.Append("...");
            return sb.ToString();
        }
    }
}