using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PS.UI.Shared.Commands
{
    public abstract class BaseCommand<T> : INotifyPropertyChanged where T : class
    {
        private bool _working;

        public BaseCommand()
        {
            Command = new DelegateCommand<T>(Execute, CanExecute, HandleException, SetWorking);
        }

        public event PropertyChangedEventHandler PropertyChanged;  //the event

        public bool IsAvailable => !Working;

        public DelegateCommand<T> Command { get; private set; }

        protected bool Working
        {
            get => _working; private set
            {
                _working = value;
                if (_working)
                    Console.WriteLine("Working...");
                else
                    Console.WriteLine("Work completed...");

                NotifyPropertyChanged(nameof(IsAvailable));
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")  //event caller
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void PreExecute() => Working = true;

        protected abstract void Execute(T parameter);

        protected virtual void PostExecute() => Working = false;

        protected virtual bool CanExecute(T parameter)
        {
            return !Working;
        }

        protected virtual void HandleException(Exception ex)
        {
            ex.Source = this.GetType().Name;
            Working = false;
            Console.WriteLine($"Error encountered: {ex.Message}");
        }

        private void SetWorking(bool workingState)
        {
            Working = workingState;
        }
    }
}