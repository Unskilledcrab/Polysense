using Microsoft.AspNetCore.Components;
using PS.UI.Shared.ViewModels;
using System;
using System.Threading.Tasks;

namespace PS.UI.Blazor
{
    public class BaseView<T> : ComponentBase, IDisposable where T : BaseViewModel
    {
        [Inject]
        public T ViewModel { get; set; }

        public void Dispose()
        {
            ViewModel.PropertyChanged -= async (sender, e) => await OnPropertyChangedHandler(sender, e);
        }

        protected override Task OnInitializedAsync()
        {
            ViewModel.PropertyChanged += async (sender, e) => await OnPropertyChangedHandler(sender, e);
            return base.OnInitializedAsync();
        }

        private async Task OnPropertyChangedHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
    }
}