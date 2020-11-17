using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace PS.UI.Shared.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        public bool IsBusy { get; set; } = false;

        public abstract Task OnUpdate();

        protected async Task Update()
        {
            IsBusy = true;
            await OnUpdate();
            IsBusy = false;
        }
    }
}