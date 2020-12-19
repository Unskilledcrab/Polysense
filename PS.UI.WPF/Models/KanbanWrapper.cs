using PS.Shared.Extensions;
using PS.Shared.Models;
using Syncfusion.UI.Xaml.Kanban;

namespace PS.UI.WPF.Models
{
    public class KanbanWrapper : KanbanModel
    {
        public KanbanWrapper(ScraperText scraperText)
        {
            ID = scraperText.Id.ToString();
            Title = scraperText.Text.GetWords(5);
            Description = scraperText.Text;
            Category = "Uncategorized";
            ColorKey = "High";
            Tags = new string[] { "Attention" };
        }
    }
}