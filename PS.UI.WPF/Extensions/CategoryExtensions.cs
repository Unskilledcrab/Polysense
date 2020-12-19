using PS.Shared.Models;
using Syncfusion.UI.Xaml.Kanban;

namespace PS.UI.WPF.Extensions
{
    public static class CategoryExtensions
    {
        public static KanbanColumn ToKanbanColumn(this TextCategory source)
        {
            return new KanbanColumn
            {
                Title = source.Name,
                Categories = source.Name
            };
        }
    }
}