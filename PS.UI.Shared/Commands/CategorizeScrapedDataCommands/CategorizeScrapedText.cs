using PS.UI.Shared.ViewModels;
using System;
using System.Threading.Tasks;

namespace PS.UI.Shared.Commands.CategorizeScrapedDataCommands
{
    public class CategorizeScrapedText : BaseCommand<CategorizeScrapedDataViewModel>
    {
        protected override async void Execute(CategorizeScrapedDataViewModel vm)
        {
            try
            {
                PreExecute();

                await Task.Delay(2000);
                //call api to save result
                await Task.Run(() =>
                {
                    Console.WriteLine($"Categorized excerpt as {vm.SelectedCategory}");

                    vm.SelectedCategory = null;
                    vm.Excerpts.GetEnumerator().MoveNext();
                    vm.SelectedExcerpt = vm.Excerpts.GetEnumerator().Current;
                });

                Console.WriteLine("Success!");
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