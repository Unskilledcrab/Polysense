using PS.Shared.Models;
using PS.Shared.Models.Abstractions;
using PS.UI.Shared.Commands.CategorizeScrapedDataCommands;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.Shared.ViewModels
{
    public class CategorizeScrapedDataViewModel : BaseViewModel
    {
        public CategorizeScrapedDataViewModel()
        {
            Excerpts.Add(new ScraperText()
            {
                Id = 0,
                Text = @"More than 205,000 new cases were reported Friday -- which likely consists of both Thursday and Friday reports in some cases, as at least 20 states did not report Covid-19 numbers on Thanksgiving.",
                Website = @"https://www.cnn.com/2020/11/28/health/us-coronavirus-saturday/index.html"
            });

            Excerpts.Add(new ScraperText()
            {
                Id = 1,
                Text = @"The US has now reported more than 100,000 infections every day for 25 consecutive days and hospitalizations remain at record high levels -- with more than 89,800 patients reported nationwide Friday, according to the COVID Tracking Project. A record was set just a day earlier, with a staggering 90,481 hospitalizations, according to the project. And the nation recorded a daily death toll of less than 1,000 only twice this week -- while the two days prior to Thanksgiving each saw more than 2,000 American deaths reported.",
                Website = @"https://www.cnn.com/2020/11/28/health/us-coronavirus-saturday/index.html"
            });

            Excerpts.Add(new ScraperText()
            {
                Id = 2,
                Text = @"And while there is more good news on the vaccine front, for now Americans need to 'hunker down' and prepare for a difficult winter ahead, according to Dr. Leana Wen, an emergency physician and a visiting professor at George Washington University Milken Institute School of Public Health.",
                Website = @"https://www.cnn.com/2020/11/28/health/us-coronavirus-saturday/index.html"
            });

            Categories.Add(new Category() { Id = 0, Label = "Opinion" });
            Categories.Add(new Category() { Id = 1, Label = "Fact" });
            Categories.Add(new Category() { Id = 2, Label = "Deflection" });
            Categories.Add(new Category() { Id = 3, Label = "Gibberish" });

            SelectedExcerpt = Excerpts.Count > 0 ? Excerpts[0] : new ScraperText();
        }

        public CategorizeScrapedDataViewModel This => this;
        public CategorizeScrapedTextCommand CategorizeScrapedTextCommand { get; private set; } = new CategorizeScrapedTextCommand();
        public string MyTitle { get; set; } = "Title";
        public ObservableCollection<ScraperText> Excerpts { get; set; } = new ObservableCollection<ScraperText>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public ScraperText SelectedExcerpt { get; set; }
        public Category SelectedCategory { get; set; }

        public async override Task OnUpdate(CancellationToken token)
        {
            await Task.Run(() =>
            {
                return;
            });
        }

        public class Category : BaseEntity
        {
            public string Label { get; set; }
        }
    }
}