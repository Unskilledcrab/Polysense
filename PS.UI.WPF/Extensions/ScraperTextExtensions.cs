using PS.Shared.Extensions;
using PS.Shared.Models;
using Syncfusion.UI.Xaml.Kanban;

namespace PS.UI.WPF.Extensions
{
    public static class ScraperTextExtensions
    {
        public static KanbanModel ToKanbanCard(this ScraperText source)
        {
            return new KanbanModel
            {
                ID = source.Id.ToString(),
                Title = source.Text,
                Description = source.Text,
                Category = source.Category.Name,
                ColorKey = source.ToColorKey(),
                Tags = new string[] { source.GetDomainName() }
            };
        }
    }
}