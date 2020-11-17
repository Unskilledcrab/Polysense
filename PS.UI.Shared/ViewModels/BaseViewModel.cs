using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PS.UI.Shared.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        public bool IsBusy { get; set; } = false;

        public abstract void OnUpdate();
        private void Update()
        {
            IsBusy = false;
            OnUpdate();
            IsBusy = true;
        }
    }
}
