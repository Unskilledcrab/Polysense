using PS.UI.Shared.ViewModels;
using System;
using System.Linq;
using System.Threading;

namespace PS.UI.Shared.Commands.CategorizeScrapedDataCommands
{
    public class GetNextScraperText : BaseCommand<CategorizeScrapedDataViewModel>
    {
        private CancellationToken token = new CancellationToken();

        protected override async void Execute(CategorizeScrapedDataViewModel vm)
        {
            try
            {
                PreExecute();

                if (vm.Excerpts == null)
                {
                    vm.Excerpts = await vm.scraperClient.GetScraperTexts(token);
                    vm.SelectedExcerpt = vm.Excerpts.FirstOrDefault();
                }
                else
                    vm.SelectedExcerpt = vm.Excerpts.Where(x => x.Id > vm.SelectedExcerpt.Id).FirstOrDefault();

                if (vm.SelectedExcerpt == null)
                {
                    vm.Excerpts = await vm.scraperClient.GetScraperTexts(token);
                    vm.SelectedExcerpt = vm.Excerpts.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered: {ex.Message}");
            }
            finally
            {
                PostExecute();
            }
        }
    }
}