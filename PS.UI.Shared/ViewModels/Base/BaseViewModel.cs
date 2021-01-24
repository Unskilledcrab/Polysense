using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PS.UI.Shared.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        public string Title { get; set; } = "No Title";
    }
}