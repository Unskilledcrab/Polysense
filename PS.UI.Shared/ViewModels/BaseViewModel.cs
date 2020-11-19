﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PS.UI.Shared.ViewModels
{
    public abstract class BaseViewModel : ObservableObject, IDisposable
    {
        private CancellationTokenSource updateTokenSource = new CancellationTokenSource();

        public BaseViewModel()
        {
            UpdateCommand = new AsyncRelayCommand(Update);
            CancelUpdateCommand = new RelayCommand(CancelUpdate);
        }

        public event EventHandler BeforeUpdate;

        public event EventHandler AfterUpdate;

        public ICommand CancelUpdateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public bool IsBusy { get; set; } = false;

        public abstract Task OnUpdate(CancellationToken token);

        public virtual async Task OnUpdateCancelled()
        {
#if DEBUG
            await Task.Delay(1);
            Console.WriteLine("An update task has been cancelled");
#endif
        }

        public void Dispose()
        {
            if (updateTokenSource != null)
            {
                updateTokenSource.Dispose();
                updateTokenSource = null;
            }
        }

        protected async Task Update()
        {
            IsBusy = true;
            ResetCancellationToken();
            var token = updateTokenSource.Token;
            try
            {
                BeforeUpdate?.Invoke(this, EventArgs.Empty);
                await OnUpdate(token);
                AfterUpdate?.Invoke(this, EventArgs.Empty);
            }
            catch (OperationCanceledException)
            {
                // log that the operation was cancelled here. this will have to be handled in each
                // viewmodel differently but the main log should go here
                await OnUpdateCancelled();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected void CancelUpdate()
        {
            updateTokenSource.Cancel();
        }

        private void ResetCancellationToken()
        {
            updateTokenSource.Cancel();
            updateTokenSource.Dispose();
            updateTokenSource = new CancellationTokenSource();
        }
    }
}