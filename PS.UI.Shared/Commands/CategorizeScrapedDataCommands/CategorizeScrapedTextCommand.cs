using PS.UI.Shared.ViewModels;
using System;
using System.Threading.Tasks;

namespace PS.UI.Shared.Commands.CategorizeScrapedDataCommands
{
    public class CategorizeScrapedTextCommand : BaseCommand<CategorizeScrapedDataViewModel>
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
                    int currentIndex = vm.Excerpts.IndexOf(vm.SelectedExcerpt);
                    int nextIndex = vm.Excerpts.Count > currentIndex + 1 ? currentIndex + 1 : 0;
                    vm.SelectedExcerpt = vm.Excerpts[nextIndex];
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